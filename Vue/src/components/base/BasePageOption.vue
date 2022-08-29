<template>
      <div class="cbb">
            <div class="combo-input">
                  <input
                        type="text "
                        class="input"
                        readonly="true"
                        v-bind:value="
                              this.pagingSelect + ' bản ghi trên 1 trang'
                        "
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
            </div>

            <div class="data" v-show="this.isShow">
                  <div
                        class="data__item"
                        v-bind:class="{
                              selected: this.pagingSelect == opt.val,
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
                        {{ opt.val }} bản ghi trên 1 trang
                  </div>
            </div>
      </div>
</template>
<script>
      import BaseButton from "./BaseButton.vue";
      import BaseIcon from "./BaseIcon.vue";
      export default {
            components: {
                  BaseButton,
                  BaseIcon,
            },
            /**
             * created (setup)
             */
            created() {
                  (this.pagingSelect = 20), this.$emit("value", 20);
            },
            data() {
                  return {
                        isShow: false,
                        paging: [
                              {
                                    val: 10,
                              },
                              {
                                    val: 20,
                              },
                              {
                                    val: 30,
                              },
                              {
                                    val: 50,
                              },
                              {
                                    val: 100,
                              },
                        ],
                        options: [],
                        pagingSelect: null,
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
                        this.options = this.paging;
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
                        this.pagingSelect = opt.val;
                        this.$emit("value", opt.val);
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
      @import url(../../assets/css/base/pageOption.css);
</style>