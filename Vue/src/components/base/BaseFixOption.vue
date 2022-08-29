<template>
      <div
            class="data-fix"
            v-show="this.isShowFix"
            :style="{ left: positionX + 'px', top: positionY + 'px' }"
      >
            <div
                  class="data__item"
                  v-bind:class="{ selected: this.isSelected == opt.option }"
                  @click="itemOnClick(opt)"
                  v-for="opt in options"
                  :key="opt.option"
            >
                  {{ opt.option }}
            </div>
      </div>
      <base-dialog
            :isShow="isShowDialog"
            :messages="msgDialog"
            @isShowDialog="functionIsShowDialog"
            iconName="icon__warm"
            :btnLeft="true"
            btnLeftName="Không"
            :btnRight="true"
            btnRight2="Có"
            @btnClick="btnClick"
      />
</template>
<script>
      import BaseDialog from "./BaseDialog.vue";
      export default {
            components: {
                  BaseDialog,
            },
            emits: ["isShowFix", "toastMsg", "replication", "delete"],
            /**
             * created (setup)
             */
            created() {
                  this.isSelected = "Nhân bản";
            },
            data() {
                  return {
                        isShow: false,
                        options: [
                              {
                                    option: "Nhân bản",
                              },
                              {
                                    option: "Xoá",
                              },
                              {
                                    option: "Ngưng sử dụng",
                              },
                        ],
                        isSelected: null,
                        isShowDialog: false,
                        msgDialog: [],
                        employee: {},
                  };
            },
            props: ["employeeSelect", "isShowFix", "positionX", "positionY"],
            methods: {
                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * click vào nút sẽ chọn option
                   */
                  itemOnClick(opt) {
                        this.isSelected = opt.option;
                        this.isShow = false;
                        this.$emit("isShowFix");
                        this.employee = this.employeeSelect;
                        if (this.isPaging) {
                              this.pagingSelect = opt.option;
                              this.$emit("value", opt.val);
                        }
                        if (this.isSelected == "Xoá") {
                              // Thông báo
                              this.msgDialog = [];
                              let msg = `Bạn có thực sự muốn xoá Nhân viên <${this.employeeSelect.employeeCode}> không?`;
                              this.msgDialog.push({ msg });
                              this.functionIsShowDialog(true);
                        } else if (this.isSelected == "Nhân bản") {
                              this.$emit("replication", this.employee);
                        }
                        this.isSelected = "Nhân bản";
                  },
                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * Hiển thị Dialog
                   */
                  functionIsShowDialog(isShow) {
                        this.isShowDialog = isShow;
                  },
                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * Bấm nút có ở dialog
                   */
                  btnClick(value) {
                        if (value == "Có") {
                              this.$emit("delete", this.employee);
                              this.employee = {};
                        }
                  },
            },
      };
</script>
<style scoped>
      @import url(../../assets/css/base/fixOption.css);
</style>