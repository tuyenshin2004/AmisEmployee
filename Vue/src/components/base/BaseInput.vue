<template>
      <base-icon v-if="hasIcon" name="icon__search" />
      <label v-if="hasLabel" for=""
            >{{ labelStr }}
            <b v-if="isRequired">(<b class="input-required"></b>)</b>
            <base-tool :content="isTitle" />
      </label>
      <input
            class="field-input"
            type="text"
            :name="labelStr"
            :class="[{ required: isRequired }, nameClass, inputClass]"
            :id="inputId"
            :placeholder="inputPlace"
            :value="modelValue"
            @input="$emit('update:modelValue', $event.target.value)"
            ref="input"
            @mouseenter="hover = true"
            @mouseleave="hover = false"
            v-on:blur="blurEvent($event)"
            v-on:keyup.enter="$event.target.blur()"
      />
      <span v-if="hover">{{ this.labelStr }} không để trống.</span>
</template>
<script>
      import baseIcon from "./BaseIcon.vue";
      import BaseTool from "./BaseTool.vue";
      export default {
            props: {
                  labelStr: String,
                  hasIcon: Boolean,
                  hasLabel: Boolean,
                  isTitle: String,
                  isRequired: Boolean,
                  inputId: String,
                  inputPlace: String,
                  iconName: String,
                  nameClass: String,
                  modelValue: String,
                  focus: {
                        type: Boolean,
                        default: false,
                  },
            },
            components: {
                  baseIcon,
                  BaseTool,
            },
            watch: {
                  focus: function (newValue) {
                        if (newValue == true) {
                              this.$nextTick(function () {
                                    this.$refs.input.focus();
                              });
                        }
                  },
            },
            Updated() {
                  this.isError = false;
            },
            data() {
                  return {
                        isError: false,
                        hover: false,
                        inputClass: "",
                  };
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
            },
            updated() {},
      };
</script>
<style scoped>
      @import url(../../assets/css/base/button.css);
      @import url(../../assets/css/base/fieldInput.css);
      .iconMargin {
            margin-left: 2px;
      }
</style>