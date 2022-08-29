using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using MISA.Amis.Demo.Model;

namespace MISA.Amis.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách dữ liệu toàn bộ nhân viên
        /// </summary>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy: LMTUYEN (29/07/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Khai báo thông tin kết nối database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.LMTUYEN; User Id = dev; Password = 12345678";

                // 1. Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);

                // 2. Lấy dữ liệu:
                // 2.1 Câu lệnh truy vấn dữ liệu
                var sqlCommand = $" SELECT * FROM Employee";

                // 2.2 Thực hiện lấy dữ liệu 
                //var employees = sqlConnection.Query<Employee>(sql: sqlCommand); // Chỉ lấy những trường có trong Employee Class
                var employees = sqlConnection.Query<object>(sql: sqlCommand); // Lấy tất cả các trường trong bảng

                // 3. Trả kết quả cho client:
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy thông tin nhân viên qua Id
        /// </summary>
        /// <param name="employeeId">Truyền vào Id của nhân viên</param>
        /// <returns>
        /// 200 - Thông tin nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy: LMTUYEN (29/07/2022)
        [HttpGet("{employeeId}")]
        public IActionResult GetById(string employeeId)
        {
            try
            {
                var error = new ErrorService();

                // Khai báo thông tin kết nối database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.LMTUYEN; User Id = dev; Password = 12345678";

                // 1. Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);

                // 2. Lấy dữ liệu:
                // 2.1 Câu lệnh truy vấn dữ liệu
                var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId = @EmployeeId";

                // Lưu ý : Nếu có tham số truyền cho câu lệnh truy vấn sql thì phải sử dụng DynamicParameters:
                // param thì dữ liệu truyền vào phải là dạng Guid
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);

                // 2.2 Thực hiện lấy dữ liệu 
                var employee = sqlConnection.QueryFirstOrDefault<object>(sql: sqlCommand, param: parameters); // Lấy tất cả các trường trong bảng
                if (employee == null)
                {
                    error.UserMsg = Resources.ResourceVN.EmployeeId_NotExist;
                    return BadRequest(error);
                }

                // 3. Trả kết quả cho client:
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>
        /// 201 - Thêm mới thành công
        /// 400 - Dữ liệu không hợp lệ
        /// 500 - Có Exception
        /// </returns>
        /// CreatedBy: LMTUYEN (29/07/2022)
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
                // Khai báo các trường cần thiết
                var error = new ErrorService();
                var errorData = new Dictionary<string, string>();

                // Bước 1: Validate dữ liệu : trả về mã 400 kèm thông tin cần thiết
                // 1.1. Thông tin mã nhân viên bắt buộc nhập
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resources.ResourceVN.Error_Validate_EmployeeCode);
                }

                // 1.2. Tên nhân viên không được để trống
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    errorData.Add("EmployeeName", Resources.ResourceVN.Error_Validate_EmployeeName);
                }

                // 1.3. Đơn vị không được để trống
                if (string.IsNullOrEmpty(employee.DepartmentId))
                {
                    errorData.Add("DepartmentId", Resources.ResourceVN.Error_Validate_DepartmentId);
                }

                // 1.4. Email đúng định dạng

                // 1.5. Ngày nhỏ hơn ngày hiện tại

                // 1.6. Mã nhân viên không được phép trùng
                if (CheckEmployeeCode(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resources.ResourceVN.Check_EmployeeCode);
                }    

                // Kiểm tra validate
                if (errorData.Count > 0)
                {
                    error.UserMsg = Resources.ResourceVN.Error_Validate_Data;
                    error.Data = errorData;
                    return BadRequest(error);
                }

                // Bước 2: Kết nối database:
                // 2.1. Khởi tạo kết nối với MariaDb:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.LMTUYEN; User Id = dev; Password = 12345678";
                var sqlConnection = new MySqlConnection(connectionString);

                // 2.2. Thên mới vào database
                var sqlCommand = "INSERT INTO Employee (EmployeeId, EmployeeCode, EmployeeName, Gender, Email, DepartmentId)" +
                                "VALUES(@EmployeeId, @EmployeeCode, @EmployeeName, @Gender, @Email, @DepartmentId)";
                // Tạo mới EmployeeId
                employee.EmployeeId = Guid.NewGuid();
                var res = sqlConnection.Execute(sql: sqlCommand, param: employee);

                // Bước 3: Trả thông tin cho client
                if(res != 0)
                {
                    return StatusCode(201);
                }    
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Kiểm tra mã nhân viên có bị trùng hay không
        /// Nếu sửa thì loại trừ chính mình ra
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <returns>true - bị trùng, false - không bị trùng</returns>
        /// CreatedBy: LMTUYEN (29/07/2022)
        private bool CheckEmployeeCode(string employeeCode)
        {
            
            // Khai báo thông tin kết nối database:
            var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.LMTUYEN; User Id = dev; Password = 12345678";
            var sqlConnection = new MySqlConnection(connectionString);
            // 2.1 Câu lệnh truy vấn dữ liệu
            var sqlCheck = $"SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @EmployeeCode";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", employeeCode);
            var res = sqlConnection.QueryFirstOrDefault<string>(sql: sqlCheck, param: parameters);
            if (res != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Xử lý ngoại lệ
        /// </summary>
        /// <param name="ex">Lỗi xảy ra</param>
        /// <returns>Trả về lỗi xảy ra</returns>
        /// CreatedBy: LMTUYEN (29/07/2022)
        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = Resources.ResourceVN.Error_Exception;
            error.Data = ex.Data;
            return StatusCode(500, error);
        }

    }
}
