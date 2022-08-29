<template>
      <div class="receipt-form">
            <div class="receipt-form__content">
                  <div class="receipt-form__header">
                        <base-icon name="icon__recent-log" size="icon--24" />
                        <div class="form-header__title">
                              {{ "Phiếu nhập kho" }}
                        </div>
                        <div class="form-header__input">
                              <base-option-simple />
                              <base-option-icon />
                        </div>
                        <div class="form-header__buttons">
                              <div class="flex">
                                    <div class="guide-tour">
                                          <base-icon
                                                name="icon__tour"
                                                size="icon--24"
                                          />
                                          <div class="tour-label">
                                                Hướng dẫn
                                          </div>
                                    </div>
                                    <base-button
                                          btnClass="btn__setting"
                                          :hasIcon="true"
                                          iconName="icon__setting"
                                          content="Cài đặt"
                                    />
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
                        </div>
                  </div>
            </div>
      </div>
</template>
<script>
      import BaseIcon from "../../../../base/BaseIcon.vue";
      import BaseOptionSimple from "../detail/base/BaseOptionSimple.vue";
      import BaseOptionIcon from "./base/BaseOptionIcon.vue";
      import BaseButton from "../../../../base/BaseButton.vue";
      export default {
            components: {
                  BaseIcon,
                  BaseOptionSimple,
                  BaseOptionIcon,
                  BaseButton,
            },
      };
</script>
<style scoped>
      @import url(../../../../../assets/css/base/pageOption.css);
      @import url(../../../../../assets/css/receiptDetail/receiptForm.css);
      @import url(../../../../../assets/css/base/baseAll.css);
</style>