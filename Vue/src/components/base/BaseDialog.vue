<template>
    <div class="dialog" id="dialog-noti" v-bind:class="{isShowDialog:isShow}">
        <div class="dialog__content">
            <div class="dialog-body">
                <base-icon :name="iconName" class="dialog-icon" />
                <div class="message-content">
                    <div class="dialog-text" v-for="message in messages" :key="message.msg">{{message.msg}}</div>
                </div>
            </div>
            <div class="dialog__footer">
                <div class="footer__left" v-if="btnLeft">
                    <!-- <button class="btn btn--green" @click="closeDialog">{{btnCenterName}}</button> -->
                    <base-button :btnTitle="btnLeftName" @click="closeDialog(btnLeftName)" />
                </div>
                <div class="footer__center" v-if="btnCenter">
                    <base-button :btnTitle="btnCenterName" btnClass="btn--green" @click="closeDialog(btnCenterName)" />
                    <!-- <button class="btn" @click="closeDialog">{{btnCenterName}}</button> -->
                </div>
                <div class="footer__right" v-if="btnRight">
                    <base-button v-if="hasThreeBtn" :btnTitle="btnRight1" @click="closeDialog(btnRight1)" />
                    <base-button :btnTitle="btnRight2" btnClass="btn--green" @click="closeDialog(btnRight2)" />
                    <!-- <button class="btn" @click="closeDialog">{{btnCenterName}}</button> -->
                </div>
                
            </div>
        </div>
    </div>
</template>
<script>
import BaseIcon from "./BaseIcon.vue";
import BaseButton from "./BaseButton.vue";
export default {
    /**
     * Lê Minh Tuyến (21/7/2022)
     */
    props: ['messages', 'isShow', "iconName", "btnLeft", "btnLeftName", "btnCenter", "btnCenterName", "btnRight", "hasThreeBtn", "btnRight1", "btnRight2", "btnClick"],
    components : {
    BaseIcon,
    BaseButton
},
    methods: {
         /**
         * Lê Minh Tuyến (21/7/2022)
         * click đóng dialog
         */
        closeDialog(btnName) {
            this.$emit("isShowDialog");
            this.$emit('btnClick', btnName);

        }
    }
}
</script>
<style scoped>
     @import url(../../assets/css/base/dialog.css);
</style>