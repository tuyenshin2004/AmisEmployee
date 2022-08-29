<template>
      <base-button
            btnClass="btn__department"
            :hasIcon="true"
            iconName="icon__dropdown--black"
            @click="showDropdown()"
      />
      <div class="dropdown__content" v-if="this.isShowDpm">
            <div class="scroll">
                  <div class="scroll__table">
                        <table class="dropdown__table">
                              <thead>
                                    <tr>
                                          <th
                                                class="
                                                      text-align-left
                                                      department-code
                                                "
                                                id="txtDepartmentCode"
                                          >
                                                Mã đơn vị
                                          </th>
                                          <th
                                                class="text-align-left"
                                                id="txtDepartmentName"
                                          >
                                                Tên đơn vị
                                          </th>
                                    </tr>
                              </thead>
                              <tbody>
                                    <tr
                                          v-for="dpm in departments"
                                          :key="dpm.departmentId"
                                          v-bind:class="{
                                                selected:
                                                      dpmSelected ==
                                                      dpm.departmentId,
                                          }"
                                          @click="departmentOnClick(dpm)"
                                    >
                                          <td
                                                class="
                                                      text-align-left
                                                      department-code
                                                "
                                          >
                                                {{ dpm.departmentCode }}
                                          </td>
                                          <td
                                                class="
                                                      text-align-left
                                                      department-name
                                                "
                                          >
                                                {{ dpm.departmentName }}
                                          </td>
                                    </tr>
                              </tbody>
                        </table>
                  </div>
            </div>
            <div class="add__department">
                  <base-icon name="icon__pen" />
                  <div>Cập nhật cơ cấu tổ chức</div>
            </div>
      </div>
</template>
<script>
      import axios from "axios";
      import BaseButton from "./BaseButton.vue";
      import BaseIcon from "./BaseIcon.vue";
      export default {
            props: {
                  dpmVal: {
                        type: String,
                        default: "",
                  },
            },
            emits: ["valDepartment"],
            components: {
                  BaseButton,
                  BaseIcon,
            },
            data() {
                  return {
                        departments: {},
                        isShowDpm: false,
                        dpmSelected: null,
                  };
            },
            /**
             * created (setup)
             */
            created() {
                  var me = this;
                  // gọi API thực thiện lấy dữ liệu -> sử dụng axios
                  axios.get("/api/v1/Departments")
                        .then(function (res) {
                              me.departments = res.data;
                        })
                        .catch(function (error) {
                              console.log(error);
                        });
            },
            methods: {
                  /**
                   * Lê Minh Tuyến (21/7/2022)
                   * border đỏ khi chưa nhập trường bắt buộc
                   */
                  blurEvent: function (e) {
                        const value = e.target.value;
                        if (this.isRequired) {
                              if (value) {
                                    // this.isError=false;
                                    this.inputClass = "";
                                    e.target.classList.remove("error");
                                    //  $(e.target).removeClass("error");
                              } else {
                                    // this.isError=true;
                                    this.inputClass = "error";
                                    e.target.classList.add("error");
                                    //  $(e.target).addClass("error");
                              }
                        }
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * click vào nút sẽ hiện option
                   */
                  showDropdown() {
                        if (this.dpmVal) {
                              this.dpmSelected = this.dpmVal;
                        }
                        if (!this.dpmSelected && this.departments.length > 0) {
                              this.dpmSelected = this.departments[0].DepartmentId;
                        }
                        if (!this.isShowDpm) {
                              this.isShowDpm = true;
                        } else {
                              this.isShowDpm = false;
                        }
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * click vào nút sẽ chọn option
                   */
                  departmentOnClick(dpm) {
                        this.dpmSelected = dpm.departmentId;
                        document
                              .getElementById("txtDepartment")
                              .classList.remove("error");
                        this.$emit("valDepartment", dpm);
                        this.isShowDpm = false;
                  },
            },
      };
</script>
<style scoped>
      @import url(../../assets/css/base/dropdown.css);
</style>