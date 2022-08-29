<template>
      <div class="combobox">
            <label v-if="isRequired"
                  >{{ this.cbbName }}
                  <b>(<b class="input-required"></b>)</b></label
            >
            <div class="combo-input">
                  <input
                        type="text"
                        class="field-input"
                        :class="{ required: isRequired }"
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
                  <span v-if="hover && isRequired">Không được để trống.</span>
                  <base-icon name="icon__search1" />
                  <base-button
                        btnClass="btn__drop-conbobox"
                        :hasIcon="true"
                        iconName="icon__arrow-dropdown"
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
                                                            v-for="field in fields"
                                                            :key="field"
                                                            :class="field.class"
                                                      >
                                                            {{ field.name }}
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
                                                                  item[
                                                                        this
                                                                              .propId
                                                                  ],
                                                            focus:
                                                                  indexItemFocus ==
                                                                  index,
                                                      }"
                                                      :ref="'toFocus_' + index"
                                                      @click="ItemOnClick(item)"
                                                      @focus="
                                                            saveItemFocus(index)
                                                      "
                                                      @keydown="
                                                            selecItemUpDown(
                                                                  index
                                                            )
                                                      "
                                                      @keyup="
                                                            selecItemUpDown(
                                                                  index
                                                            )
                                                      "
                                                      tabindex="1"
                                                >
                                                      <td
                                                            v-for="data in dataFilter"
                                                            :key="data"
                                                            :class="field.class"
                                                      >
                                                            {{ item[data] }}
                                                      </td>
                                                </tr>
                                          </tbody>
                                    </table>
                              </div>
                        </div>
                        <div class="footer-cbb" v-if="false">
                              <base-icon name="icon__pen" />
                              <div>Cập nhật cơ cấu tổ chức</div>
                        </div>
                  </div>
            </div>
      </div>
</template>
<script>
      import BaseButton from "../../../../../base/BaseButton.vue";
      import BaseIcon from "../../../../../base/BaseIcon.vue";
      export default {
            props: [
                  "isShowForm",
                  "isRequired",
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
                        fields: [
                              { name: "Số chứng từ", class: "", nick: "number" },
                              { name: "Ngày chứng từ", class: "", nick: "date" },
                              { name: "Chi nhánh", class: "", nick: "branch" },
                        ],
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
            created() {},
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
                        } else if (this.isRequired) {
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
      @import url(../../../../../../assets/css/base/combobox.css);

      .combo-input {
            display: flex;
            align-items: center;
            width: 100%;
      }
      .field-input {
            width: 100%;
            height: 32px;
            border-radius: 4px;
            padding: 0 16px 0 16px;
            box-sizing: border-box;
            border: solid 1px #bbbb;
            cursor: default;
            background-color: transparent;
            width: 100%;
            align-items: center;
            padding: 0 0 0 10px;
      }

      .btn__drop-conbobox {
            /* padding: 12px;
                        height: 30px !important;
                        width: 32px;
                        border-radius: 0 4px 4px 0;
                        border: none;
                        background-color: transparent;
                        cursor: pointer;
                        padding: 6px 10px !important;
                        outline: none;
                        margin-left: -32px; */
      }
      .btn__drop-conbobox:focus {
            background-color: #e9ebee;
      }
      .icon__search1 {
      }
</style>