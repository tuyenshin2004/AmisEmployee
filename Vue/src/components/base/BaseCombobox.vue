<template>
      <div class="combobox">
            <label
                  >{{ this.cbbName }}
                  <b>(<b class="input-required"></b>)</b></label
            >
            <input
                  type="text"
                  class="field-input required"
                  :name="cbbName"
                  :id="inputId"
                  v-model="text"
                  @input="inputOnChange"
                  @keydown="selecItemUpDown"
                  @mouseenter="hover = true"
                  @mouseleave="hover = false"
                  v-on:blur="blurEvent($event)"
                  v-on:keyup.enter="$event.target.blur()"
            />
            <span v-if="hover">Đơn vị không được để trống.</span>
            <base-button
                  btnClass="btn__combobox"
                  :hasIcon="true"
                  iconName="icon__dropdown--black"
                  @click="showCbbData()"
                  @keydown="selecItemUpDown"
                  tabindex="-1"
            />
            <div
                  class="combobox__content"
                  v-if="isShowCbb"
                  ref="combobox__data"
            >
                  <div class="scroll">
                        <div class="scroll__table">
                              <table class="combobox__table">
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
                                                v-for="(
                                                      item, index
                                                ) in dataFilter"
                                                :key="item[this.propId]"
                                                v-bind:class="{
                                                      selected:
                                                            itemSelected ==
                                                            item[this.propId],
                                                      focus:
                                                            indexItemFocus ==
                                                            index,
                                                }"
                                                :ref="'toFocus_' + index"
                                                @click="ItemOnClick(item)"
                                                @focus="saveItemFocus(index)"
                                                @keydown="
                                                      selecItemUpDown(index)
                                                "
                                                @keyup="selecItemUpDown(index)"
                                                tabindex="1"
                                          >
                                                <td
                                                      class="
                                                            text-align-left
                                                            department-code
                                                      "
                                                >
                                                      {{ item[this.propCode] }}
                                                </td>
                                                <td
                                                      class="
                                                            text-align-left
                                                            department-name
                                                      "
                                                >
                                                      {{ item[this.propName] }}
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
      </div>
</template>
<script>
      import axios from "axios";
      import BaseButton from "./BaseButton.vue";
      import BaseIcon from "./BaseIcon.vue";
      export default {
            props: [
                  "isShowForm",
                  "valueIdCbb",
                  "valueNameCbb",
                  "cbbName",
                  "inputId",
                  "propId",
                  "propCode",
                  "propName",
            ],
            emits: ["valCombobox"],
            components: {
                  BaseButton,
                  BaseIcon,
            },
            data() {
                  return {
                        data: {},
                        dataFilter: {},
                        isShowCbb: false,
                        itemSelected: null,
                        indexItemFocus: null,
                        text: null,
                        hover: false,
                  };
            },
            watch: {
                  valueNameCbb: function (newVal) {
                        // watch it
                        this.text = newVal;
                  },
                  isShowForm: function (newVal) {
                        if (newVal == false) {
                              this.isShowCbb = false;
                        }
                  },
            },
            /**
             * created (setup)
             */
            created() {
                  var me = this;
                  // gọi API thực thiện lấy dữ liệu -> sử dụng axios
                  axios.get("/api/v1/Departments")
                        .then(function (res) {
                              me.data = res.data;
                              me.dataFilter = res.data;
                        })
                        .catch(function () {});

                  me.data = [
                        {
                              departmentId: "1",
                              departmentCode: "PHC",
                              departmentName: "Phòng Hành Chính",
                        },
                        {
                              departmentId: "2",
                              departmentCode: "PNS",
                              departmentName: "Phòng Nhân Sự",
                        },
                        {
                              departmentId: "3",
                              departmentCode: "PKT",
                              departmentName: "Phòng Kế toán",
                        },
                  ];
                  me.dataFilter = me.data;
            },
            methods: {
                  /**
                   * Lê Minh Tuyến (21/7/2022)
                   * border đỏ khi chưa nhập trường bắt buộc
                   */
                  blurEvent: function (e) {
                        const value = e.target.value;
                        if (value) {
                              this.inputClass = "";
                              e.target.classList.remove("error");
                              //  $(e.target).removeClass("error");
                        } else {
                              this.inputClass = "error";
                              e.target.classList.add("error");
                              //  $(e.target).addClass("error");
                        }
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * click vào nút sẽ hiện option
                   */
                  showCbbData() {
                        if (this.valueIdCbb) {
                              this.itemSelected = this.valueIdCbb;
                        }
                        if (!this.valueIdCbb) {
                              this.itemSelected = null;
                              this.elToFocus = null;
                        }
                        if (!this.itemSelected && this.data.length > 0) {
                              this.itemSelected = this.data[0][this.propId];
                        }
                        if (!this.isShowCbb) {
                              this.isShowCbb = true;
                              this.dataFilter = this.data;
                        } else {
                              this.isShowCbb = false;
                        }
                  },
                  /**
                   * Lê Minh Tuyến (24/7/2022)
                   * click vào nút sẽ chọn option
                   */
                  ItemOnClick(dpm) {
                        this.itemSelected = dpm[this.propId];
                        document
                              .getElementById(this.inputId)
                              .classList.remove("error");
                        this.$emit("valCombobox", dpm);
                        this.text = dpm[this.propName];
                        this.isShowCbb = false;
                  },
                  inputOnChange() {
                        this.isShowCbb = true;
                        // Thực hiện lọc các phần tử phù hợp trong data:
                        var me = this;
                        this.dataFilter = this.data.filter((e) => {
                              return (
                                    e[this.propName].includes(me.text) ||
                                    e[this.propCode].includes(me.text)
                              );
                        });
                  },
                  /**
                   * Lưu lại vị trí focus
                   */
                  saveItemFocus(index) {
                        this.indexItemFocus = index;
                  },
                  /**
                   * Chọn lên xuống
                   */
                  selecItemUpDown() {
                        var me = this;
                        var keyCode = event.keyCode;
                        var elToFocus = null;
                        switch (keyCode) {
                              case 27:
                                    //escape
                                    this.isShowCbb = false;
                                    break;
                              case 40:
                                    //down arrow
                                    this.isShowCbb = true;
                                    elToFocus =
                                          this.$refs[
                                                `toFocus_${me.indexItemFocus + 1}`
                                          ];
                                    if (
                                          this.indexItemFocus == null ||
                                          !elToFocus ||
                                          elToFocus.length == 0
                                    ) {
                                          this.indexItemFocus = 0;
                                    } else {
                                          this.indexItemFocus += 1;
                                    }
                                    break;
                              case 38:
                                    // up arrow
                                    this.isShowCbb = true;
                                    elToFocus =
                                          this.$refs[
                                                `toFocus_${me.indexItemFocus - 1}`
                                          ];
                                    if (
                                          this.indexItemFocus == null ||
                                          !elToFocus ||
                                          elToFocus.length == 0
                                    ) {
                                          this.indexItemFocus =
                                                this.dataFilter.length - 1;
                                    } else {
                                          this.indexItemFocus -= 1;
                                    }
                                    break;
                              case 13:
                                    // enter
                                    elToFocus =
                                          this.$refs[`toFocus_${me.indexItemFocus}`];
                                    if (elToFocus && elToFocus.length > 0) {
                                          elToFocus[0].click();
                                          if (me.indexItemFocus >= 0) {
                                                me.ItemOnClick(
                                                      me.data[me.indexItemFocus]
                                                );
                                                me.indexItemFocus = null;
                                          }
                                          me.isShowCbb = false;
                                    }
                                    break;
                              default:
                                    break;
                        }
                  },
            },
      };
</script>
<style scoped>
      @import url(../../assets/css/base/combobox.css);
</style>