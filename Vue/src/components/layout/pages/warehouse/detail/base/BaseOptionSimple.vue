<template>
      <div class="cbb">
            <div class="combo-input">
                  <input
                        type="text "
                        class="input"
                        readonly="true"
                        v-bind:value="this.itemSelect"
                  />
                  <base-button
                        btnClass="button"
                        @click="showOption()"
                        @keydown="selectItemUpDown"
                  >
                        <base-icon
                              size="icon--24"
                              name="icon__arrow-dropdown"
                        />
                  </base-button>
                  <div class="data" v-show="this.isShow">
                        <div
                              class="data__item"
                              v-bind:class="{
                                    selected: this.itemSelect == opt.val,
                                    focus: this.indexItemFocus == index,
                              }"
                              @click="itemOnClick(opt)"
                              v-for="(opt, index) in options"
                              :key="opt.option"
                              :ref="'toFocus_' + index"
                              @focus="saveItemFocus(index)"
                              @keydown="selectItemUpDown(index)"
                              @keyup="selectItemUpDown(index)"
                              tabindex="1"
                        >
                              {{ opt.val }}
                        </div>
                  </div>
            </div>
      </div>
</template>
<script>
      import BaseButton from "../../../../../base/BaseButton.vue";
      import BaseIcon from "../../../../../base/BaseIcon.vue";
      export default {
            components: {
                  BaseButton,
                  BaseIcon,
            },
            /**
             * created (setup)
             */
            created() {
                  this.itemSelect = "1. Thành phẩm sản xuất";
            },
            data() {
                  return {
                        isShow: false,
                        items: [
                              {
                                    val: "1. Thành phẩm sản xuất",
                              },
                              {
                                    val: "2. Hàng bị trả lại",
                              },
                              {
                                    val: "3. Khác (NVL thừa, HH thuê gia công, ...)",
                              },
                        ],
                        options: [],
                        itemSelect: null,
                        indexItemFocus: null,
                  };
            },
            props: ["value"],
            methods: {
                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * click vào nút sẽ hiện option
                   */
                  showOption() {
                        this.options = this.items;
                        if (!this.isShow) {
                              this.isShow = true;
                        } else {
                              this.isShow = false;
                        }
                  },
                  /**
                   * Lê Minh Tuyến (22/7/2022)
                   * click vào nút sẽ chọn option
                   */
                  itemOnClick(opt) {
                        this.isSelected = opt.val;
                        this.indexItemFocus = null;
                        this.isShow = false;
                        this.itemSelect = opt.val;
                        // this.$emit("value", opt.val);
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
                  selectItemUpDown() {
                        var me = this;
                        var keyCode = event.keyCode;
                        var elToFocus = null;
                        switch (keyCode) {
                              case 27:
                                    //escape
                                    this.isShow = false;
                                    break;
                              case 40:
                                    //down arrow
                                    this.isShow = true;
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
                                    this.isShow = true;
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
                                                this.options.length - 1;
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
                                                // me.ItemOnClick(
                                                //     me.options[me.indexItemFocus]
                                                // );
                                                me.indexItemFocus = null;
                                          }
                                          this.showOption();
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
      @import url(../../../../../../assets/css/base/pageOption.css);
      .cbb {
            margin: 0px 10px 0px 0px;
            height: 30px;
            border-radius: 4px;
            position: relative;
            min-width: 250px;
      }
      .cbb .data {
            position: absolute;
            width: 100%;
            top: 110%;
            left: 0%;
            background-color: #ffff;
            z-index: 999;
            border: solid 1px #bbbb;
      }
      .input {
            width: 100%;
            align-items: center;
            padding: 0 0 0 10px;
      }
      .combo-input .button {
            margin-left: -32px;
      }
      .data {
            width: 100% !important;
      }
</style>