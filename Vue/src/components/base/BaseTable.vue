<template>
      <div class="scroll-table">
            <table class="table" id="employeeList">
                  <thead>
                        <tr>
                              <th
                                    class="
                                          text-align-center
                                          row-checkbox
                                          checkbox-padding
                                    "
                              >
                                    <!-- <base-checkbox /> -->
                                    <!-- <input type="checkbox" @click='checkAll()' /> -->
                                    <label class="container-checkbox">
                                          <input
                                                type="checkbox"
                                                class="checkbox"
                                                id="chkAll"
                                                @click="checkAll($event)"
                                          />
                                          <span class="checkmark"></span>
                                    </label>
                              </th>
                              <th
                                    v-for="head in headers"
                                    :key="head.text"
                                    :class="head.class"
                              >
                                    {{ head.text }}
                              </th>
                        </tr>
                  </thead>
                  <tbody>
                        <tr
                              v-for="emp in bodyData"
                              :key="emp.employeeId"
                              class="row"
                              @dblclick="rowOnDblClick(emp)"
                        >
                              <td
                                    class="
                                          text-align-center
                                          row-checkbox
                                          checkbox-padding
                                    "
                              >
                                    <!-- <base-checkbox /> -->
                                    <label class="container-checkbox">
                                          <input
                                                type="checkbox"
                                                class="checkbox chk"
                                                @change="
                                                      changeCheck(
                                                            emp.employeeId,
                                                            $event
                                                      )
                                                "
                                          />
                                          <span class="checkmark"></span>
                                    </label>
                              </td>
                              <td class="text-align-left row-code">
                                    {{ emp.employeeCode }}
                              </td>
                              <td class="text-align-left">
                                    {{ emp.employeeName }}
                              </td>
                              <td class="text-align-left">
                                    {{ emp.genderName }}
                              </td>
                              <td class="text-align-center">
                                    {{ formatDate(emp.dateOfBirth) }}
                              </td>
                              <td class="text-align-left">
                                    {{ emp.identityNumber }}
                              </td>
                              <td class="text-align-left">
                                    {{ emp.positionName }}
                              </td>
                              <td class="text-align-left">
                                    {{ emp.departmentName }}
                              </td>
                              <td class="text-align-left">
                                    {{ emp.bankAccountNumber }}
                              </td>
                              <td class="text-align-left">
                                    {{ emp.bankName }}
                              </td>
                              <td class="text-align-left">
                                    {{ emp.bankBranchName }}
                              </td>
                              <td class="text-align-center row-function">
                                    <div class="fix">
                                          <div class="option">Sửa</div>
                                          <base-button
                                                btnClass="btn__fix"
                                                @click="
                                                      showFixOption($event, emp)
                                                "
                                          >
                                                <base-icon
                                                      size="icon--24"
                                                      name="icon__down"
                                                />
                                          </base-button>
                                    </div>
                              </td>
                        </tr>
                  </tbody>
            </table>
      </div>
      <!-- <div v-bind:class="{noneData:isNoneData}" class="hasData">

    </div> -->
      <base-fix-option
            :isShowFix="isShowFix"
            :employeeSelect="rowFix"
            @isShowFix="FixSelected()"
            :positionX="leftFix"
            :positionY="topFix"
            @toastMsg="toastMassage"
            @replication="replicationEmp"
            @delete="deleteEmp"
      />
</template>
<script>
      // import BaseCheckbox from './BaseCheckbox.vue';
      import BaseFixOption from "./BaseFixOption.vue";
      import BaseButton from "./BaseButton.vue";
      import BaseIcon from "./BaseIcon.vue";
      export default {
            props: {
                  bodyData: {
                        type: Array,
                  },
                  headers: {
                        type: Array,
                  },
                  isDelete: {
                        type: Boolean,
                  },
            },
            emits: [
                  "rowSelected",
                  "addToastMsg",
                  "replicationEmployee",
                  "deleteEmployee",
                  "rowsSelected",
                  "deleted",
            ],
            // props: ["bodyData", "headers", "isNoneData"],
            components: {
                  // BaseCheckbox,
                  BaseFixOption,
                  BaseButton,
                  BaseIcon,
            },
            data() {
                  return {
                        isShowFix: false,
                        rowFix: {},
                        leftFix: null,
                        topFix: null,
                        checkList: [],
                  };
            },
            methods: {
                  /**
                   * Lê Minh Tuyến (14/7/2022)
                   * Định dạng ngày/tháng/năm cho bảng
                   * @param {*} date
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
                   * Lê Minh Tuyến (29/7/2022)
                   * Bấm đúp chuột vào hàng trên bảng
                   * form nhập sẽ hiện lên kèm các giá trị
                   * @param {*} employee
                   */
                  rowOnDblClick(emp) {
                        this.$emit("rowSelected", emp);
                  },
                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * click vào nút sẽ hiện option
                   * @param {*} e
                   * @param {*} rowData
                   */
                  showFixOption(e, rowData) {
                        if (this.isShowFix) {
                              if (this.rowFix == rowData) {
                                    this.isShowFix = false;
                              } else {
                                    this.rowFix = rowData;
                              }
                        } else {
                              this.isShowFix = true;
                              this.rowFix = rowData;
                        }
                        this.leftFix = e.clientX - 120;
                        this.topFix = e.clientY + 15;
                  },
                  /**
                   * Lê Minh Tuyến (1/8/2022)
                   * click chọn thì đóng option
                   */
                  FixSelected() {
                        this.isShowFix = false;
                        this.rowFix = {};
                  },
                  /**
                   * Lê Minh Tuyến (2/8//2022)
                   * Gửi toastMsg
                   * @param {} content
                   */
                  toastMassage(content) {
                        this.$emit("addToastMsg", content);
                  },

                  /**
                   * Lê Minh Tuyến (14/8//2022)
                   * Xoá nhân viên
                   * @param {*} empId
                   */
                  deleteEmp(emp) {
                        this.$emit("deleteEmployee", emp);
                  },

                  /**
                   * Lê Minh Tuyến (11/8//2022)
                   * Nhân bản nhân viên
                   * @param {*} emp
                   */
                  replicationEmp(emp) {
                        this.$emit("replicationEmployee", emp);
                  },

                  /**
                   * Lê Minh Tuyến (14/8//2022)
                   * Chọn tất cả checkbox
                   */
                  checkAll(e) {
                        this.checkList = [];
                        let chks = document.getElementsByClassName("chk");
                        let tr = document.querySelectorAll(".row");
                        if (e.target.checked) {
                              for (let i = 0; i < this.bodyData.length; i++) {
                                    this.checkList.push(this.bodyData[i].employeeId);
                              }
                              for (let j = 0; j < chks.length; j++) {
                                    chks[j].checked = "checked";
                                    tr[j].classList.add("row-selected");
                              }
                        } else {
                              for (let j = 0; j < chks.length; j++) {
                                    chks[j].checked = "";
                                    tr[j].classList.remove("row-selected");
                              }
                        }
                        //  console.log(this.checkList);
                        this.$emit("rowsSelected", this.checkList);
                  },

                  /**
                   * Lê Minh Tuyến (14/8//2022)
                   * Thay đổi trạng thái checkbox
                   * @param {*} empId
                   * @param {*} e event
                   */
                  changeCheck(empId, e) {
                        if (this.isDelete == true) {
                              this.checkList = [];
                              this.$emit("rowsSelected", this.checkList);
                              this.$emit("deleted", false);
                        }
                        if (e.target.checked) {
                              this.checkList.push(empId);
                              e.target.parentNode.parentNode.parentNode.classList.add(
                                    "row-selected"
                              );
                        } else {
                              this.checkList.splice(this.checkList.indexOf(empId), 1);
                              e.target.parentNode.parentNode.parentNode.classList.remove(
                                    "row-selected"
                              );
                        }
                        let chks = document.getElementsByClassName("chk");
                        if (this.checkList.length == chks.length) {
                              document.getElementById("chkAll").checked = "checked";
                        } else {
                              document.getElementById("chkAll").checked = "";
                        }
                        //console.log(this.checkList);
                        this.$emit("rowsSelected", this.checkList);
                  },
            },
      };
</script>
<style scoped>
      @import url(../../assets/css/page/content/table.css);
      @import url(../../assets/css/base/checkbox.css);
      .fix {
            display: flex;
            align-items: center;
            padding: 0px 20px 0px 20px;
      }

      .fix .option {
            color: #0075c0;
      }
      .hasData {
            display: none;
      }
      .noneData {
            display: block;
      }
</style>