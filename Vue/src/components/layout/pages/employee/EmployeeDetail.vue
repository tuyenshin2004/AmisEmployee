<template>
      <div class="form" id="formInfo" v-bind:class="{ isShowForm: isShow }">
            <div class="form__content">
                  <div class="form__btn">
                        <base-button
                              btnClass="btn__ask"
                              :hasIcon="true"
                              iconName="icon__ask"
                              @click="btnAskOnClick()"
                              content="Hỏi đáp"
                        />
                        <base-button
                              btnClass="btn__close"
                              :hasIcon="true"
                              iconName="icon__close"
                              @click="btnCloseOnClick()"
                              content="Đóng"
                        />
                  </div>
                  <div class="form__header">
                        <div class="form-header__title">
                              Thông tin nhân viên
                        </div>
                        <div class="form-header__checkbox">
                              <base-checkbox
                                    nameSlot="Là khách hàng"
                                    :hasSpan="true"
                              />
                        </div>
                        <div class="form-header__checkbox">
                              <base-checkbox
                                    nameSlot="Là nhà cung cấp"
                                    :hasSpan="true"
                              />
                        </div>
                  </div>
                  <div class="form__body">
                        <div class="form__top">
                              <div class="form-top__left">
                                    <div class="form-row">
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtCode"
                                                      v-model="
                                                            employee.employeeCode
                                                      "
                                                      :hasLabel="true"
                                                      :isRequired="true"
                                                      labelStr="Mã"
                                                      :focus="isFocus"
                                                />
                                          </div>
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtFullName"
                                                      v-model="
                                                            employee.employeeName
                                                      "
                                                      :hasLabel="true"
                                                      :isRequired="true"
                                                      labelStr="Tên"
                                                />
                                          </div>
                                    </div>
                                    <div class="form-row">
                                          <div class="label-input">
                                                <!-- <div class="dropdown">
                                                      <div
                                                            class="
                                                                  dropdown__input
                                                            "
                                                      >
                                                            <base-input
                                                                  inputId="txtDepartment"
                                                                  v-model="
                                                                        employee.departmentName
                                                                  "
                                                                  :hasLabel="
                                                                        true
                                                                  "
                                                                  :isRequired="
                                                                        true
                                                                  "
                                                                  labelStr="Đơn vị"
                                                            />
                                                      </div>
                                                      <base-dropdown
                                                            :dpmVal="
                                                                  employee.departmentId
                                                            "
                                                            @valDepartment="
                                                                  changeValDepartment
                                                            "
                                                      />
                                                </div> -->
                                                <base-combobox
                                                      :isShowForm="isShow"
                                                      :valueNameCbb="
                                                            employee.departmentName
                                                      "
                                                      :valueIdCbb="
                                                            employee.departmentId
                                                      "
                                                      @valCombobox="
                                                            changeValDepartment
                                                      "
                                                      cbbName="Đơn vị"
                                                      inputId="txtDepartment"
                                                      propId="departmentId"
                                                      propCode="departmentCode"
                                                      propName="departmentName"
                                                />
                                          </div>
                                    </div>
                                    <div class="form-row">
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtPosition"
                                                      v-model="
                                                            employee.positionName
                                                      "
                                                      :hasLabel="true"
                                                      labelStr="Chức danh"
                                                />
                                          </div>
                                    </div>
                              </div>
                              <div class="form-top__right">
                                    <div class="form-row">
                                          <div class="label-input">
                                                <!-- <base-input inputId="dbDoB" v-model="employee.DateOfBirth" :hasLabel="true" labelStr="Ngày sinh" inputPlace="DD/MM/YYYY" /> -->
                                                <label for="">Ngày sinh</label
                                                ><br />
                                                <date-picker
                                                      :lang="vi"
                                                      v-model:value="
                                                            employee.dateOfBirth
                                                      "
                                                      value-type="DD/MM/YYYY"
                                                      format="DD/MM/YYYY"
                                                      placeholder="DD/MM/YYYY"
                                                      input-class="inputDate "
                                                ></date-picker>

                                                <!-- <input v-model="employee.DateOfBirth" class="field-input" name="Ngày sinh" id="dbDoB" type="text" placeholder="DD/MM/YYYY" onfocus="(this.type='date')" onblur="(this.type='text')"> -->
                                          </div>
                                          <div class="label-input gender">
                                                <label for=" ">Giới tính </label
                                                ><br />
                                                <div
                                                      class="gender-radio"
                                                      id="txtGender"
                                                >
                                                      <!-- <BaseRadio nameRadio="Nam" hasChecked="true" /> -->
                                                      <base-radio
                                                            :genderVal="
                                                                  employee.gender
                                                            "
                                                            @genderSelect="
                                                                  genderSelectRadio
                                                            "
                                                      />
                                                </div>
                                          </div>
                                    </div>
                                    <div class="form-row">
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtCardID"
                                                      v-model="
                                                            employee.identityNumber
                                                      "
                                                      :hasLabel="true"
                                                      labelStr="Số CCCD"
                                                      isTitle="Căn cước công dân"
                                                />
                                          </div>
                                          <div
                                                class="label-input"
                                                id="dbDateIssue"
                                          >
                                                <label for="">Ngày cấp</label
                                                ><br />
                                                <date-picker
                                                      v-model:value="
                                                            employee.identityDate
                                                      "
                                                      value-type="DD/MM/YYYY"
                                                      format="DD/MM/YYYY"
                                                      placeholder="DD/MM/YYYY"
                                                      input-class="inputDate "
                                                ></date-picker>
                                          </div>
                                    </div>
                                    <div class="form-row">
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtIssue"
                                                      v-model="
                                                            employee.identityPlace
                                                      "
                                                      :hasLabel="true"
                                                      labelStr="Nơi cấp"
                                                />
                                          </div>
                                    </div>
                              </div>
                        </div>
                        <div class="form__tabs-container">
                              <div class="tabs">
                                    <div class="form-tabs__tab">
                                          <p>Liên hệ</p>
                                    </div>
                              </div>
                              <div class="tab__content">
                                    <div class="form-row">
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtAddress"
                                                      v-model="employee.address"
                                                      :hasLabel="true"
                                                      labelStr="Địa chỉ"
                                                />
                                          </div>
                                    </div>
                                    <div class="form-row contact">
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtMobilePhone"
                                                      v-model="
                                                            employee.phoneNumber
                                                      "
                                                      :hasLabel="true"
                                                      labelStr="Điện thoại di động"
                                                />
                                          </div>
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtLandlinePhone"
                                                      v-model="
                                                            employee.telephoneNumber
                                                      "
                                                      :hasLabel="true"
                                                      labelStr="Điện thoại cố định"
                                                />
                                          </div>
                                          <div class="label-input">
                                                <base-input
                                                      v-model="employee.email"
                                                      inputId="txtEmail"
                                                      :hasLabel="true"
                                                      labelStr="Email"
                                                />
                                          </div>
                                    </div>
                                    <div class="form-row bank">
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtBankNumber"
                                                      v-model="
                                                            employee.bankAccountNumber
                                                      "
                                                      :hasLabel="true"
                                                      labelStr="Tài khoản ngân hàng"
                                                />
                                          </div>
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtBankName"
                                                      v-model="
                                                            employee.bankName
                                                      "
                                                      :hasLabel="true"
                                                      labelStr="Tên ngân hàng"
                                                />
                                          </div>
                                          <div class="label-input">
                                                <base-input
                                                      inputId="txtBranchName"
                                                      v-model="
                                                            employee.bankBranchName
                                                      "
                                                      :hasLabel="true"
                                                      labelStr="Chi nhánh"
                                                />
                                          </div>
                                    </div>
                              </div>
                        </div>
                  </div>
                  <div class="form__footer">
                        <div class="footer__left">
                              <base-button
                                    btnTitle="Huỷ"
                                    id="btn-cancel"
                                    @click="btnCannelOnClick()"
                              />
                        </div>
                        <div class="footer__right">
                              <base-button
                                    btnTitle="Cất"
                                    id="btn-save"
                                    @click="btnSaveOnClick()"
                              />
                              <base-button
                                    btnTitle="Cất và Thêm"
                                    id="btn-saveAndAdd"
                                    @click="btnSaveAddClick()"
                                    btnClass="btn--green"
                              />
                        </div>
                  </div>
            </div>
      </div>
      <base-dialog
            :isShow="isShowDialog"
            :messages="message"
            @isShowDialog="functionIsShowDialog"
            :iconName="iconMsg"
            :btnCenter="btnDialog.btnCenter"
            :btnCenterName="btnDialog.btnCenterName"
            :btnLeft="btnDialog.btnLeft"
            :btnLeftName="btnDialog.btnLeftName"
            :btnRight="btnDialog.btnRight"
            :hasThreeBtn="btnDialog.hasThreeBtn"
            :btnRight1="btnDialog.btnRight1"
            :btnRight2="btnDialog.btnRight2"
            @btnClick="btnClick"
      />
</template>
<script>
      import axios from "axios";
      import { vi } from "vue-datepicker-next/locale/vi";
      import DatePicker from "vue-datepicker-next";
      import "vue-datepicker-next/index.css";
      import FormMode from "../../../../emuns/modules/formMode.js";
      import BaseButton from "../../../base/BaseButton.vue";
      import BaseCheckbox from "../../../base/BaseCheckbox.vue";
      import BaseInput from "../../../base/BaseInput.vue";
      //   import BaseDropdown from "../../../base/BaseDropdown.vue";
      import BaseRadio from "../../../base/BaseRadio.vue";
      import BaseDialog from "@/components/base/BaseDialog.vue";
      import BaseCombobox from "../../../base/BaseCombobox.vue";

      export default {
            components: {
                  BaseButton,
                  BaseCheckbox,
                  BaseInput,
                  //   BaseDropdown,
                  BaseRadio,
                  BaseDialog,
                  DatePicker,
                  BaseCombobox,
            },
            emits: [
                  "isShowForm",
                  "changeMode",
                  "changeEmployee",
                  "toastMsg",
                  "loading",
            ],
            // computed : {
            //     formatCode() {
            //         return this.employee.EmployeeCode ? this.employee.EmployeeCode : '';
            //     },
            // },
            data() {
                  return {
                        employee: {},
                        message: [],
                        iconMsg: "",
                        err: false,
                        count: 0,
                        isShowDialog: false,
                        btnDialog: {
                              btnCenter: false,
                              btnLeft: false,
                              btnRight: false,
                              hasThreeBtn: false,
                              btnLeftName: "",
                              btnCenterName: "",
                              btnRight1: "",
                              btnRight2: "",
                        },
                        vi: vi,
                  };
            },
            props: ["isShow", "employeeSelected", "formMode", "isFocus"],
            updated() {
                  this.employee = this.employeeSelected;
            },
            methods: {
                  /**
                   * Lê Minh Tuyến (14/7/2022)
                   * Bấm vào nút X trên form
                   * Đóng form nhập
                   */
                  btnCloseOnClick() {
                        this.defaultDialog();
                        let msg = "Dữ liệu bị thay đổi, bạn có muốn cất không?";
                        this.message.push({ msg });
                        this.iconMsg = "icon__info-dialog";
                        this.btnDialog.btnLeft = true;
                        this.btnDialog.btnLeftName = "Huỷ";
                        this.btnDialog.btnRight = true;
                        this.btnDialog.hasThreeBtn = true;
                        this.btnDialog.btnRight1 = "Không";
                        this.btnDialog.btnRight2 = "Có";
                        this.functionIsShowDialog(true);
                  },

                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * Đặt lại mặc định cho dialog
                   */
                  defaultDialog() {
                        this.message = [];
                        (this.btnDialog.btnCenter = false),
                              (this.btnDialog.btnLeft = false),
                              (this.btnDialog.btnRight = false),
                              (this.btnDialog.hasThreeBtn = false),
                              (this.btnDialog.btnLeftName = ""),
                              (this.btnDialog.btnCenterName = ""),
                              (this.btnDialog.btnRight1 = ""),
                              (this.btnDialog.btnRight2 = "");
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * Bấm vào nút Huỷ trên form
                   * Đóng form nhập
                   */
                  btnCannelOnClick() {
                        this.$emit("isShowForm");
                        this.$emit("isShowForm", false);
                  },
                  /**
                   * Lê Minh Tuyến (16/7/2022)
                   * Bấm nút Cất
                   * Lưu dữ liệu và đóng form
                   */
                  btnSaveOnClick() {
                        // loading
                        this.$emit("loading", true);
                        // 1.Validate dữ liệu
                        this.count = 0;
                        this.checkRequired();
                        // thực hiện gọi API
                        this.SaveEmployee(1);
                  },
                  /**
                   * Lê Minh Tuyến (16/7/2022)
                   * Bấm nút Cất
                   * Lưu dữ liệu và thêm mới
                   */
                  btnSaveAddClick() {
                        // loading
                        this.$emit("loading", true);
                        // 1.Validate dữ liệu
                        this.count = 0;
                        this.checkRequired();
                        // thực hiện gọi API
                        this.SaveEmployee(2);
                  },

                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * Lưu thông tin nhân viên
                   * @param {} mode : form mode
                   */
                  SaveEmployee(mode) {
                        var me = this;
                        // Kiểm tra có lỗi hay không
                        if (!me.err) {
                              if (!me.err) {
                                    let dOB = me.employee.dateOfBirth;
                                    let ident = me.employee.identityDate;
                                    // đổi dateOfBirth
                                    me.employee.dateOfBirth = me.functionFormatDate(
                                          me.employee.dateOfBirth
                                    );
                                    // đổi identityDate
                                    me.employee.identityDate = me.functionFormatDate(
                                          me.employee.identityDate
                                    );
                                    //Convert mã nhân viên
                                    me.employee.employeeText =
                                          me.employee.employeeCode.substring(0, 2);
                                    me.employee.employeeNumber = parseInt(
                                          me.employee.employeeCode.substring(
                                                3,
                                                me.employee.employeeCode.length
                                          )
                                    );
                                    //Convert Giới tính
                                    if (me.employee.gender == null) {
                                          me.employee.gender = 2;
                                    }

                                    // 3.Gọi API để lưu
                                    // 3.1. Kiểm tra trạng thái form là thêm mới hay sửa
                                    if (me.formMode == FormMode.EDIT) {
                                          axios.put(
                                                `api/v1/Employees/${me.employee.employeeId}`,
                                                me.employee
                                          )
                                                .then(function () {
                                                      me.$emit("toastMsg", {
                                                            icon: "success",
                                                            msg: "Sửa thành công.",
                                                      });
                                                      // 1 là cất , 2 là cất và thêm
                                                      if (mode == 1) {
                                                            me.$emit("isShowForm");
                                                            me.$emit(
                                                                  "isShowForm",
                                                                  false
                                                            );
                                                      } else {
                                                            me.defaultForm();
                                                            me.$emit("changeMode");
                                                      }
                                                      me.$emit("loading", false);
                                                })
                                                .catch(function (error) {
                                                      me.employee.dateOfBirth = dOB;
                                                      me.employee.identityDate =
                                                            ident;
                                                      me.$emit("loading", false);
                                                      switch (error.response.status) {
                                                            case 400:
                                                                  if (
                                                                        error.response
                                                                              .data
                                                                              .userMsg ==
                                                                        `Mã nhân viên <${me.employee.employeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`
                                                                  ) {
                                                                        me.DuplicateCode(
                                                                              error
                                                                                    .response
                                                                                    .data
                                                                                    .userMsg
                                                                        );
                                                                        break;
                                                                  } else {
                                                                        me.$emit(
                                                                              "toastMsg",
                                                                              {
                                                                                    icon: "error",
                                                                                    msg: error
                                                                                          .response
                                                                                          .data
                                                                                          .userMsg,
                                                                              }
                                                                        );
                                                                        break;
                                                                  }
                                                            case 500:
                                                                  me.$emit(
                                                                        "toastMsg",
                                                                        {
                                                                              icon: "error",
                                                                              msg: "Server hiện đang bị lỗi.",
                                                                        }
                                                                  );
                                                                  break;
                                                            default:
                                                                  break;
                                                      }
                                                });
                                    } else {
                                          axios.post(`/api/v1/Employees`, me.employee)
                                                .then(function () {
                                                      me.$emit("toastMsg", {
                                                            icon: "success",
                                                            msg: "Thêm mới thành công.",
                                                      });
                                                      // 1 là cất , 2 là cất và thêm
                                                      if (mode == 1) {
                                                            me.$emit("isShowForm");
                                                            me.$emit(
                                                                  "isShowForm",
                                                                  false
                                                            );
                                                      } else {
                                                            me.defaultForm();
                                                            me.$emit("changeMode");
                                                      }
                                                      me.$emit("loading", false);
                                                })
                                                .catch(function (error) {
                                                      me.employee.dateOfBirth = dOB;
                                                      me.employee.identityDate =
                                                            ident;
                                                      me.$emit("loading", false);
                                                      switch (error.response.status) {
                                                            case 400:
                                                                  if (
                                                                        error.response
                                                                              .data
                                                                              .userMsg ==
                                                                        `Mã nhân viên <${me.employee.employeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`
                                                                  ) {
                                                                        me.DuplicateCode(
                                                                              error
                                                                                    .response
                                                                                    .data
                                                                                    .userMsg
                                                                        );
                                                                        break;
                                                                  } else {
                                                                        me.$emit(
                                                                              "toastMsg",
                                                                              {
                                                                                    icon: "error",
                                                                                    msg: error
                                                                                          .response
                                                                                          .data
                                                                                          .userMsg,
                                                                              }
                                                                        );
                                                                        break;
                                                                  }
                                                            case 500:
                                                                  me.$emit(
                                                                        "toastMsg",
                                                                        {
                                                                              icon: "error",
                                                                              msg: "Server hiện đang bị lỗi.",
                                                                        }
                                                                  );
                                                                  break;
                                                            default:
                                                                  break;
                                                      }
                                                });
                                    }
                              }
                        }
                  },
                  // fomat lại date
                  functionFormatDate(date) {
                        if (date) {
                              var dd = date.slice(0, 2);
                              var mm = date.slice(3, 5);
                              var yyyy = date.slice(6, 10);
                              var newDate = mm + "/" + dd + "/" + yyyy;
                              newDate = new Date(newDate);
                              newDate.setDate(newDate.getDate() + 1);
                              newDate = newDate.toJSON();
                              return newDate;
                        }
                        return null;
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * quay lại mặc định của form nhập
                   */
                  defaultForm() {
                        this.$emit("changeEmployee");
                  },
                  /**
                   * Lê Minh Tuyến (21/7/2022)
                   * Kiểm tra các trường bắt buộc nhập
                   */
                  checkRequired() {
                        this.defaultDialog();
                        var inputRequires = document.querySelectorAll(".required");
                        inputRequires.forEach((input) => {
                              if (!input.value) {
                                    let msg =
                                          input.getAttribute("name") +
                                          " không được bỏ trống.";
                                    this.message.push({ msg });
                                    this.count++;
                                    input.classList.add("error");
                              }
                              if (input.value) {
                                    input.classList.remove("error");
                              }
                        });
                        if (this.employee.employeeCode != null) {
                              const regex = new RegExp(/^([a-zA-Z]){2}-([0-9])+$/);
                              if (!regex.test(this.employee.employeeCode)) {
                                    let msg =
                                          "Mã nhân viên phải có định dạng [Chữ][Chữ]-[Các số] .";
                                    this.message.push({ msg });
                                    this.count++;
                                    document
                                          .getElementById("txtCode")
                                          .classList.add("error");
                              }
                        }
                        if (this.count > 0) {
                              this.iconMsg = "icon__error";
                              this.btnDialog.btnCenter = true;
                              this.btnDialog.btnCenterName = "Đóng";
                              this.$emit("loading", false);
                              this.functionIsShowDialog(true);
                              this.err = true;
                        } else {
                              this.err = false;
                              this.defaultDialog();
                        }
                  },
                  /**
                   * Lê Minh Tuyến (11/8/2022)
                   * Kiểm tra mã nhân viên trùng
                   * @param {*} userMsgDuplicateCode : thông báo khi nhân bản
                   */
                  DuplicateCode(userMsgDuplicateCode) {
                        this.defaultDialog();
                        let msg = userMsgDuplicateCode;
                        this.message.push({ msg });
                        this.iconMsg = "icon__warm";
                        this.btnDialog.btnRight = true;
                        this.btnDialog.btnRight2 = "Đồng ý";
                        this.functionIsShowDialog(true);
                        this.err = true;
                  },
                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * Hiển thị loading
                   */
                  functionIsShowDialog(isShow) {
                        this.isShowDialog = isShow;
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * Bấm nút có ở dialog
                   * @param {value} : Tên của button được chọn
                   */
                  btnClick(value) {
                        if (value == "Không") {
                              this.$emit("isShowForm");
                              this.$emit("isShowForm", false);
                        }
                        if (value == "Có") {
                              this.checkRequired();
                              this.SaveEmployee(1);
                        }
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * Thay đổi value của input department
                   * @param {val} : Đơn vị được chọn
                   */
                  changeValDepartment(val) {
                        if (val.departmentName) {
                              this.employee.departmentName = val.departmentName;
                        }
                        this.employee.departmentCode = val.departmentCode;
                        this.employee.departmentId = val.departmentId;
                        var dpm = document.getElementById("txtDepartment");
                        if (dpm.value) {
                              dpm.classList.remove("error");
                        }
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * Chọn giới tính
                   * @param {gender} : Giới tính được chọn
                   */
                  genderSelectRadio(gender) {
                        (this.employee.gender = gender.id),
                              (this.employee.genderName = gender.name);
                  },

                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * Bấm vào nút 'Hỏi' trên form nhập
                   */
                  btnAskOnClick() {
                        this.$emit("toastMsg", {
                              icon: "warm",
                              msg: "Chưa thực hiện chức năng này.",
                        });
                  },
            },
      };
</script>
<style>
      @import url(../../../../assets/css/employeeDetail/formInfo.css);
      .mx-datepicker {
            margin-top: 8px !important;
            height: 30px !important;
            padding-bottom: 1px;
            box-sizing: border-box;
            width: 100% !important;
      }
      .inputDate {
            height: 30px !important;
            padding: 0px 12px 0px 10px !important;
            display: inline-block;
            box-sizing: border-box;
            width: 100%;
            font-size: 13px;
            color: #555;
            border: 1px solid #bbbbbb;
            border-radius: 4px;
            outline: none;
      }
      .inputDate:focus {
            border-color: #35bf22 !important;
      }

      .mx-calendar-content .cell.active {
            color: #08bf1e !important;
            background-color: rgba(44, 160, 28, 0.2) !important;
            border-radius: 50%;
      }
      .mx-calendar-content .cell:hover {
            /* border: 1px solid #2ca01c !important; */
            border-radius: 50% !important;
            background-color: #dddd !important;
      }
      .mx-table tbody > tr:hover td {
            cursor: pointer;
            background-color: transparent !important;
      }
      .mx-calendar-content .cell.today {
            color: #08bf1e !important;
      }
</style>