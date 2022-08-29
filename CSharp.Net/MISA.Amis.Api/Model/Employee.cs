namespace MISA.Amis.Demo.Model
{
    public class Employee
    {

        public Guid EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public int? Gender { get; set; }

        public string? GenderName 
        {  
            get
            {
                switch(Gender)
                {
                    case 0:
                        return "Nữ";
                    case 1:
                        return "Nam";
                    default:
                        return "Chưa xác định";
                }    
            }
        }

        public string? Email { get; set; }

        public string DepartmentId { get; set; }

        public string? DepartmentCode { get; set; }

        public string? DepartmentName { get; set; }

    }
}
