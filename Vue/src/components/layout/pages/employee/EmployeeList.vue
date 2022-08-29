<template>
      <div class="page">
            <div class="page-header">
                  <div class="page-header__content">
                        <div class="page-header__title">Nhân viên</div>
                        <div class="padding__mid"></div>
                        <div class="add-button">
                              <base-button
                                    btnTitle="Tiện ích"
                                    btnClass="btn__dropdown"
                                    :hasIcon="true"
                                    iconName="icon__dropdown--black"
                              />
                              <base-button
                                    btnTitle="Thêm"
                                    btnClass="btn__add btn--green"
                                    id="btnAddEmployee"
                                    @click="btnAddOnClick"
                              />
                              <base-button
                                    btnClass="btn__dropdown btn--green"
                                    :hasIcon="true"
                                    iconName="icon__dropdown--white"
                              />
                        </div>
                  </div>
                  <div class="back-all pointer">
                        <base-icon size="icon--24" name="icon__back" />
                        <a>Tất cả danh mục</a>
                  </div>
            </div>
            <div class="page-content">
                  <div class="page-content__toolbar">
                        <base-button
                              btnTitle="Thực hiện hàng loạt"
                              btnClass="btn__dropdown"
                              :hasIcon="true"
                              iconName="icon__dropdown--black"
                              @click="btnShowDelete"
                        />
                        <base-button
                              v-if="showDelete"
                              btnTitle="Xoá"
                              btnClass="btn__delete"
                              @click="btnBulkDelete"
                        />
                        <div class="padding-mid"></div>
                        <div class="search">
                              <base-icon name="icon__search" />
                              <input
                                    class="field-input"
                                    id="inputSearch"
                                    placeholder="Tìm kiếm theo Mã, Tên, hoặc Số điện thoại"
                                    @keyup="employeeFilter($event)"
                              />
                              <!-- <base-input inputId="inputSearch" nameClass="input" :hasIcon="true" iconName="icon__search" inputPlace="Tìm kiếm theo Mã, Tên, hoặc Số điện thoại" /> -->
                        </div>
                        <div class="toolbar__icon">
                              <base-tool content="Tải lại trang" />
                              <base-icon
                                    class="icon__refresh"
                                    size="icon--24"
                                    @click="refreshOnClick()"
                              />
                        </div>

                        <div class="toolbar__icon">
                              <base-tool content="Xuất Excel" />
                              <base-icon
                                    class="icon__excel"
                                    size="icon--24"
                                    @click="exportOnclick()"
                              />
                        </div>
                        <div class="toolbar__icon">
                              <base-tool content="Cài đặt" />
                              <base-icon
                                    class="icon__setting-list"
                                    size="icon--24"
                                    @click="settingOnCLick()"
                              />
                        </div>
                  </div>
                  <!-- table danh sách và thông tin nhân viên -->
                  <div class="page-table">
                        <base-table
                              :headers="headers"
                              :bodyData="employees"
                              @rowSelected="rowOnDblClick"
                              @addToastMsg="addToastMsg"
                              @replicationEmployee="functionReplication"
                              @deleteEmployee="functionDeleteEmp"
                              @rowsSelected="functionListSelected"
                              :isDelete="deletedBulk"
                              @deleted="changeDeleteBulk"
                        />
                        <div class="paging">
                              <!-- left -->
                              <div class="paging__left">
                                    Tổng số <b>{{ total }}</b> bản ghi
                              </div>
                              <!-- right -->
                              <div class="paging__right">
                                    <div class="paging-select">
                                          <base-page-option
                                                :isPaging="true"
                                                @value="employeeOfPage"
                                          />
                                    </div>
                                    <!-- <BasePaging
                        :currentPage=currentPage
                        @pageChanged=onPageChange
                        :totalPages="Math.ceil(total / numEmployeeOfPage)"
                        :perPage=numEmployeeOfPage
                        :total=total
                        /> -->
                                    <vue-awesome-paginate
                                          :total-items="total"
                                          :items-per-page="numEmployeeOfPage"
                                          :max-pages-shown="5"
                                          :current-page="currentPage"
                                          :on-click="onClickHandler"
                                          prev-button-content="Trước"
                                          next-button-content="Sau"
                                    />
                              </div>
                        </div>
                  </div>
            </div>
      </div>
      <employee-detail
            :isShow="isShowForm"
            :employeeSelected="employeeSelected"
            @changeEmployee="changeEmployeeSelect"
            @changeMode="changeFormMode"
            :formMode="formMode"
            @isShowForm="functionIsShowForm"
            @toastMsg="addToastMsg"
            :isFocus="isFocusEmpCode"
            @loading="showLoading"
      />
      <base-loading
            :isShow="isShowLoading"
            @isShowLoading="functionIsShowLoading"
      />
      <base-toast />
      <base-dialog
            :isShow="isShowDialog"
            :messages="msgDialog"
            @isShowDialog="functionIsShowDialog"
            iconName="icon__warm"
            :btnLeft="true"
            btnLeftName="Không"
            :btnRight="true"
            btnRight2="Có"
            @btnClick="btnDialogClick"
      />
</template>
  
<script>
      import axios from "axios";
      import FormMode from "../../../../emuns/modules/formMode.js";
      import EmployeeDetail from "./EmployeeDetail.vue";
      import BaseButton from "../../../base/BaseButton.vue";
      import BaseIcon from "../../../base/BaseIcon.vue";
      import BasePageOption from "../../../base/BasePageOption.vue";
      import BaseLoading from "../../../base/BaseLoading.vue";
      // import BasePaging from "../../../base/BasePaging.vue";
      import BaseTable from "../../../base/BaseTable.vue";
      import BaseToast from "../../../base/BaseToast.vue";
      import BaseDialog from "../../../base/BaseDialog.vue";
      import BaseTool from "../../../base/BaseTool.vue";

      export default {
            components: {
                  EmployeeDetail,
                  BaseButton,
                  BaseIcon,
                  BasePageOption,
                  // BasePaging,
                  BaseLoading,
                  BaseTable,
                  BaseToast,
                  BaseDialog,
                  BaseTool,
            },

            /**
             * created (setup)
             */
            created() {
                  // var me = this;
                  // me.functionIsShowLoading(true);
                  // // gọi API thực thiện lấy dữ liệu -> sử dụng axios
                  // axios.get(`/api/v1/Employees`)
                  // .then(function (res) {
                  //     me.employees = res.data;
                  //     me.total = me.employees.length;
                  //     me.functionIsShowLoading(false);
                  // })
                  // .catch(function (error) {
                  //     console.log(error);
                  //     me.functionIsShowLoading(false);
                  // });
                  this.pagination();
            },
            data() {
                  return {
                        fullName: "lê Minh Tuyến",
                        employees: [],
                        total: 0,
                        currentPage: 1,
                        numEmployeeOfPage: 10,
                        isShowForm: false,
                        employeeSelected: null,
                        employeeSelectedId: null,
                        formMode: 0,
                        isFix: false,
                        isShowLoading: false,
                        isInputErr: true,
                        headers: [
                              {
                                    text: "MÃ NHÂN VIÊN",
                                    class: "text-align-left row-code",
                              },
                              {
                                    text: "TÊN NHÂN VIÊN",
                                    class: "text-align-left row-name",
                              },
                              {
                                    text: "GIỚI TÍNH",
                                    class: "text-align-left row-gender",
                              },
                              {
                                    text: "NGÀY SINH",
                                    class: "text-align-center row-date",
                              },
                              {
                                    text: "Số CMND",
                                    class: "text-align-left row-identity",
                              },
                              {
                                    text: "CHỨC DANH",
                                    class: "text-align-left row-position",
                              },
                              {
                                    text: "ĐƠN VỊ",
                                    class: "text-align-left row-department",
                              },
                              {
                                    text: "SỐ TÀI KHOẢN",
                                    class: "text-align-left row-account-number",
                              },
                              {
                                    text: "TÊN NGÂN HÀNG",
                                    class: "text-align-left row-bank",
                              },
                              {
                                    text: "CHI NHÁNH TK NGÂN HÀNG",
                                    class: "text-align-left row-branch",
                              },
                              {
                                    text: "CHỨC NĂNG",
                                    class: "text-align-center row-function",
                              },
                        ],
                        timeOut: null,
                        showDelete: false,
                        employeesSelectedId: [],
                        deletedBulk: false,
                        isFocusEmpCode: false,
                        isShowDialog: false,
                        msgDialog: [],
                  };
            },
            methods: {
                  /**
                   * Lê Minh Tuyến (14/7/2022)
                   * Bấm chuột vào nút thêm
                   */
                  btnAddOnClick() {
                        // document.getElementById("formInfo").style.display = "block";
                        this.inputDefault();
                        this.functionIsShowForm(true);
                        this.changeEmployeeSelect();
                        this.formMode = FormMode.ADD;
                  },

                  /**
                   * Lê Minh Tuyến (14/7/2022)
                   * Bấm đúp chuột vào hàng trên bảng
                   * form nhập sẽ hiện lên kèm các giá trị
                   * @param {} emp : truyền vào nhân viên được chọn
                   */
                  rowOnDblClick(emp) {
                        this.inputDefault();
                        clearTimeout(this.timeOut);

                        this.timeOut = setTimeout(() => {
                              this.employeeSelected = emp;
                              this.getEmployee(emp.employeeId);
                              this.functionIsShowForm(true);
                              this.formMode = FormMode.EDIT;
                        }, 500);
                  },
                  /**
                   * Lê Minh Tuyến (14/7/2022)
                   * Định dạng ngày/tháng/năm cho bảng
                   * @param {} date
                   */
                  formatDate(date) {
                        try {
                              if (date) {
                                    // convert về Date
                                    date = new Date(date);
                                    let newDate = ("0" + date.getDate()).slice(-2);
                                    let month = ("0" + (date.getMonth() + 1)).slice(
                                          -2
                                    );
                                    let year = date.getFullYear();
                                    return `${newDate}/${month}/${year}`;
                              }
                        } catch (error) {
                              console.log(error);
                              return "";
                        }
                  },

                  /**
                   * Lê Minh Tuyến (19/7/2022)
                   * Lấy về thông tin 1 nhân viên quan id
                   * @param {} employeeID : truyền vào Id của nhân viên được chọn
                   */
                  getEmployee(employeeID) {
                        var me = this;
                        // loading
                        me.functionIsShowLoading(true);
                        axios.get(`/api/v1/Employees/${employeeID}`)
                              .then(function (res) {
                                    me.employeeSelected = res.data;
                                    me.employeeSelected.dateOfBirth = me.formatDate(
                                          res.data.dateOfBirth
                                    );
                                    me.employeeSelected.identityDate = me.formatDate(
                                          res.data.identityDate
                                    );
                                    // if (res.data.departmentId) {
                                    //       me.getDepartment(res.data.departmentId);
                                    // }
                                    me.functionIsShowLoading(false);
                              })
                              .catch(function () {
                                    me.addToastMsg({
                                          icon: "error",
                                          msg: "Đã xảy ra lỗi, vui lòng liên hệ MISA để được hỗ trợ.",
                                    });
                                    me.functionIsShowLoading(false);
                                    me.functionIsShowLoading(false);
                              });
                  },

                  /**
                   * Lê Minh Tuyến (19/7/2022)
                   * lấy về 1 mã nhân viên mới
                   */
                  getEmployeeCode() {
                        // loading
                        var me = this;
                        // loading
                        me.functionIsShowLoading(true);
                        // Gọi API lấy mã nhân viên mới
                        axios.get("/api/v1/Employees/NewEmployeeCode")
                              .then(function (res) {
                                    me.employeeSelected.employeeCode = res.data;
                                    me.isFocusEmpCode = true;
                                    me.functionIsShowLoading(false);
                              })
                              .catch(function () {
                                    me.addToastMsg({
                                          icon: "error",
                                          msg: "Đã xảy ra lỗi, vui lòng liên hệ MISA để được hỗ trợ.",
                                    });
                                    me.functionIsShowLoading(false);
                                    me.functionIsShowLoading(false);
                              });
                  },

                  /**
                   * Lê Minh Tuyến (14/7/2022)
                   * Hiển thị form nhập
                   * @param {*} isShow
                   */

                  functionIsShowForm(isShow) {
                        this.isShowForm = isShow;
                        if (isShow == false) {
                              this.isFocusEmpCode = false;
                              this.pagination();
                        } else {
                              this.isFocusEmpCode = true;
                        }
                  },
                  /**
                   * Lê Minh Tuyến (23/7/2022)
                   * Hiển thị loading
                   * @param {*} isShow
                   */
                  functionIsShowLoading(isShow) {
                        this.isShowLoading = isShow;
                        // this.$refs.employeeCode.focus();
                  },
                  /**
                   * Lê Minh Tuyến (30/7/2022)
                   * ShowLoading từ form nhập liệu
                   */
                  showLoading(isShow) {
                        this.functionIsShowLoading(isShow);
                  },

                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * Hiển thị Dialog
                   */
                  functionIsShowDialog(isShow) {
                        this.isShowDialog = isShow;
                  },
                  /**
                   * Lê Minh Tuyến (23/7/2022)
                   * Bấm nút refresh để load lại toàn bộ dữ liệu
                   */
                  refreshOnClick() {
                        this.pagination();
                  },
                  /**
                   * Lê Minh Tuyến (23/7/2022)
                   * Để các input về default
                   */
                  inputDefault() {
                        var inputRequires = document.querySelectorAll(".required");
                        inputRequires.forEach((input) => {
                              input.classList.remove("error");
                        });
                  },
                  /**
                   * Lê Minh Tuyến (23/7/2022)
                   * cập nhật số nhân viên trên 1 trang
                   * @param {*} value: Số nhân viên / 1 trang
                   */
                  employeeOfPage(value) {
                        this.numEmployeeOfPage = value;
                        this.currentPage = 1;
                        this.pagination();
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * thay đổi data trên page
                   */
                  // onPageChange(page) {
                  //     this.currentPage = page;
                  //     this.employeeFilter();
                  // },

                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * đặt nhân viên được lựa chọn là {}
                   */
                  changeEmployeeSelect() {
                        this.employeeSelected = {};
                        this.getEmployeeCode();
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * đặt lại form mode
                   */
                  changeFormMode() {
                        this.formMode = 0;
                  },

                  /**
                   * Lê Minh Tuyến (28/7/2022)
                   * Tìm kiến trên bảng
                   */
                  employeeFilter(e) {
                        clearTimeout(this.timeOut);

                        this.timeOut = setTimeout(() => {
                              var me = this;
                              if (e.target.value) {
                                    me.currentPage = 1;
                                    me.functionIsShowLoading(true);
                                    // gọi API thực thiện lấy dữ liệu -> sử dụng axios
                                    axios.get(
                                          `api/v1/Employees/Filter?pageSize=${me.numEmployeeOfPage}&pageNumber=${me.currentPage}&employeeFilter=${e.target.value}`
                                    )
                                          .then(function (res) {
                                                me.employees = res.data.data;
                                                me.total = res.data.totalRecord;
                                                me.functionIsShowLoading(false);
                                          })
                                          .catch(function () {
                                                me.addToastMsg({
                                                      icon: "error",
                                                      msg: "Đã xảy ra lỗi, vui lòng liên hệ MISA để được hỗ trợ.",
                                                });
                                                me.functionIsShowLoading(false);
                                          });
                              } else {
                                    me.pagination();
                              }
                        }, 1000);
                  },

                  /**
                   * Lê Minh Tuyến (2/8/2022)
                   * Hàm lấy dữ liệu  phânn trang
                   */
                  pagination() {
                        var me = this;
                        me.functionIsShowLoading(true);
                        // gọi API thực thiện lấy dữ liệu -> sử dụng axios
                        axios.get(
                              `api/v1/Employees/Filter?pageSize=${me.numEmployeeOfPage}&pageNumber=${me.currentPage}`
                        )
                              .then(function (res) {
                                    me.employees = res.data.data;
                                    me.total = res.data.totalRecord;
                                    me.functionIsShowLoading(false);
                              })
                              .catch(function (error) {
                                    switch (error.response.status) {
                                          case 400:
                                                me.addToastMsg({
                                                      icon: "error",
                                                      msg: error.response.data
                                                            .userMsg,
                                                });
                                                break;
                                          case 500:
                                                me.addToastMsg({
                                                      icon: "error",
                                                      msg: "Đã xảy ra lỗi, vui lòng liên hệ MISA để được hỗ trợ.",
                                                });
                                                break;
                                          default:
                                                break;
                                    }

                                    me.functionIsShowLoading(false);
                              });
                  },

                  /**
                   * Lê Minh Tuyến (2/08/2022)
                   * Phân trang
                   * @param {} page : chọn số trang
                   */
                  onClickHandler(page) {
                        this.currentPage = page;
                        this.deletedBulk = true;
                        this.employeesSelectedId = [];
                        this.pagination();
                  },
                  /**
                   * Lê Minh Tuyến (2/8/2022)
                   * Hiển thị toast Msg
                   * @param {} content: icon và thông điệp được gửi đến
                   */
                  addToastMsg(content) {
                        const main = document.getElementById("toast");
                        if (main) {
                              const toast = document.createElement("div");
                              // Auto remove toast
                              setTimeout(function () {
                                    main.removeChild(toast);
                              }, 2000);

                              const icons = {
                                    success: "icon icon__toast-success",
                                    error: "icon icon__toast-error",
                                    warm: "icon icon__toast-warm",
                                    info: "icon icon__toast-info",
                              };

                              toast.classList.add("toast-item");
                              toast.style.animation = `slideInLeft ease .3s, fadeOut linear 1s 2s forwards`;

                              toast.innerHTML = `
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              <div class="toast-item__icon">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  <div class="icon ${
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        icons[
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              content
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    .icon
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  }"></div>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              </div>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              <div class="toast-item__text">${
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    content.msg
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              }</div>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          `;
                              main.appendChild(toast);
                        }
                  },

                  /**
                   * Lê Minh Tuyến (11/8/2022)
                   * Nhân bản 1 nhân viên
                   * @param {*} emp : thông tin nhân viên
                   */
                  functionReplication(emp) {
                        this.inputDefault();
                        this.getEmployee(emp.employeeId);
                        clearTimeout(this.timeOut);

                        this.timeOut = setTimeout(() => {
                              this.getEmployeeCode();
                              this.functionIsShowForm(true);
                              this.formMode = FormMode.ADD;
                        }, 1000);
                  },

                  /**
                   * Lê Minh Tuyến (11/8/2022)
                   * Xoá 1 nhân viên
                   * @param {*} emp : thông tin nhân viên
                   */
                  functionDeleteEmp(emp) {
                        clearTimeout(this.timeOut);

                        this.timeOut = setTimeout(() => {
                              var me = this;
                              // gọi API thực thiện xoá dữ liệu
                              axios.delete(`/api/v1/Employees/${emp.employeeId}`)
                                    .then(function () {
                                          let message =
                                                "Đã xoá nhân viên " +
                                                emp.employeeCode;
                                          // Toast msg
                                          // me.$emit('toastMsg',{icon: "success", msg: massage});
                                          me.addToastMsg({
                                                icon: "success",
                                                msg: message,
                                          });
                                          me.pagination();
                                    })
                                    .catch(function (error) {
                                          let message = error.response.data.userMsg;
                                          // Toast msg
                                          // me.$emit('toastMsg',{icon: "success", msg: massage});
                                          me.addToastMsg({
                                                icon: "success",
                                                msg: message,
                                          });
                                          me.pagination();
                                    });
                        }, 500);
                  },

                  /**
                   * Lê Minh Tuyến (15/8/2022)
                   * Danh sách được chọn
                   * @param {} listId : danh sách các ID của nhân viên được chọn
                   */
                  functionListSelected(listId) {
                        this.employeesSelectedId = listId;
                  },

                  /**
                   * Lê Minh Tuyến (15/8/2022)
                   * Hiện ra nút xoá để xoá hàng loạt
                   */
                  btnShowDelete() {
                        if (this.showDelete) this.showDelete = false;
                        else this.showDelete = true;
                  },

                  /**
                   * Lê Minh Tuyến (15/8/2022)
                   * Thực hiện xoá nhiều
                   */
                  btnBulkDelete() {
                        this.showDelete = false;
                        // Thông báo
                        this.msgDialog = [];
                        let msg = "";
                        if (this.employeesSelectedId.length == 1) {
                              msg = `Bạn có thực sự muốn xoá nhân viên này không?`;
                              this.msgDialog.push({ msg });
                              this.functionIsShowDialog(true);
                        } else if (this.employeesSelectedId.length > 1) {
                              msg = `Bạn có thực sự muốn xoá ${this.employeesSelectedId.length} nhân viên này không?`;
                              this.msgDialog.push({ msg });
                              this.functionIsShowDialog(true);
                        } else {
                              this.addToastMsg({
                                    icon: "info",
                                    msg: "Bạn chưa chọn nhân viên nào.",
                              });
                        }
                  },

                  /**
                   * Thay đổi biến delete
                   */
                  changeDeleteBulk(val) {
                        this.deletedBulk = val;
                  },

                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * Bấm nút có ở dialog
                   */
                  btnDialogClick(value) {
                        var count = 0;
                        if (value == "Có") {
                              var me = this;
                              if (me.employeesSelectedId.length > 0) {
                                    me.functionIsShowLoading(true);
                              }
                              for (
                                    let i = 0;
                                    i < me.employeesSelectedId.length;
                                    i++
                              ) {
                                    let id = me.employeesSelectedId[i];
                                    // gọi API thực hiện xoá dữ liệu
                                    axios.delete(`/api/v1/Employees/${id}`)
                                          .then(function () {
                                                count++;
                                                if (
                                                      count ==
                                                      me.employeesSelectedId.length
                                                ) {
                                                      me.pagination();
                                                      let message =
                                                            "Đã xoá " +
                                                            count +
                                                            " nhân viên.";
                                                      // Toast msg
                                                      me.addToastMsg({
                                                            icon: "success",
                                                            msg: message,
                                                      });
                                                      me.deletedBulk = true;
                                                      me.functionIsShowLoading(false);
                                                }
                                          })
                                          .catch(function (error) {
                                                let message =
                                                      error.response.data.userMsg;
                                                // Toast msg
                                                // me.$emit('toastMsg',{icon: "success", msg: massage});
                                                me.addToastMsg({
                                                      icon: "success",
                                                      msg: message,
                                                });
                                                me.pagination();
                                                me.functionIsShowLoading(false);
                                          });
                              }
                        }
                  },

                  /**
                   * Lê Minh Tuyến (17/8/2022)
                   * Xuất Excel
                   */
                  exportOnclick() {
                        this.functionIsShowLoading(true);
                        var me = this;
                        // gọi API thực thiện xoá dữ liệu
                        axios.get(`/api/v1/Employees/Export`, {
                              responseType: "blob",
                        })
                              .then(function (res) {
                                    me.functionIsShowLoading(false);
                                    const url = URL.createObjectURL(
                                          new Blob([res.data], {
                                                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                          })
                                    );
                                    const link = document.createElement("a");
                                    link.href = url;
                                    link.setAttribute(
                                          "download",
                                          "DanhSachNhanVien.xlsx"
                                    );
                                    document.body.appendChild(link);
                                    link.click();
                              })
                              .catch(function () {
                                    me.functionIsShowLoading(false);
                                    me.addToastMsg({
                                          icon: "error",
                                          msg: "Đã xảy ra lỗi, vui lòng liên hệ MISA để được hỗ trợ.",
                                    });
                              });
                  },

                  settingOnCLick() {
                        this.addToastMsg({
                              icon: "warm",
                              msg: "Chưa thực hiện tính năng này",
                        });
                  },
            },
      };
</script>
<style>
      @import url(../../../../assets/css/page.css);
      @import url(../../../../assets/css/page/pageHeader.css);
      @import url(../../../../assets/css/page/pageContent.css);
      @import url(../../../../assets/css/page/content/toolbar.css);
      @import url(../../../../assets/css/page/content/paging.css);
      .icon__excel:hover {
            background-color: #dddddd;
      }
      .icon__refresh:hover {
            background-color: #dddddd;
      }
      .icon__setting-list:hover {
            background-color: #dddddd;
      }
      .pagination-container {
            column-gap: 4px;
            height: 30px;
            align-items: center;
      }
      .paginate-buttons {
            height: 20px;
            width: 20px;
            /* border-radius: 20px; */
            cursor: pointer;
            border: none;
            background-color: transparent;
            padding: 0 !important;
            /* background-color: rgb(242, 242, 242); */
            /* border: 1px solid rgb(217, 217, 217); */
            color: rgb(62, 62, 62);
      }
      .paginate-buttons:hover {
            background-color: #eeee;
      }

      .back-button,
      .next-button {
            width: 40px !important;
      }
      .active-page {
            border: solid 1px #bbbbbb;
            color: black;
            font-weight: bold;
      }
      .active-page:hover {
            background-color: #eeee;
      }
</style>