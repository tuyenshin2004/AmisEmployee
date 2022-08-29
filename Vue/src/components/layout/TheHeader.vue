<template>
    <div class="header">
        <div class="header__bars pointer" >
            <base-icon class="icon__bars" size="icon--24" @click="sideBarOnClick()" />
        </div>
        <div class="header__name">Lê Minh Tuyến</div>
        <div class="header__icon--down pointer">
            <base-icon class="icon__option-down" size="icon--24" />
        </div>
        <div class="header__data-name pointer">
            <base-icon class="icon__current-db" />
            <div class="db-name">DC-03</div>
        </div>
        <div class="padding__mid"> </div>
        <div class="user-icon pointer">
            <base-icon class="icon__download" size="icon--24" />
        </div>
        <div class="header__search">
            <base-input  nameClass="input" :hasIcon="true" iconName="icon__search" inputPlace="Nhập từ khoá tìm kiếm" />
        </div>
        <div class="user-icon pointer">
            <base-icon class="icon__category" size="icon--24" />
        </div>
        <div class="user-icon pointer">
             <base-icon class="icon__setting" size="icon--24" />
        </div>
        <div class="user-icon pointer">
             <base-icon class="icon__chat" size="icon--24" />
        </div>
        <div class="user-icon pointer">
             <base-icon class="icon__help" size="icon--24" />
        </div>
        <div class="user-icon pointer">
             <base-icon class="icon__info" size="icon--24" />
        </div>
        <div class="user-icon pointer">
            <base-icon class="icon__notification" size="icon--24" />
        </div>
        <base-icon class="icon__avatar" size="icon--24" />
        <div class="user-item__name">Lê Minh Tuyến</div>
        <base-icon class="icon__option" size="icon--24" />
    </div>
    <Menu :isCloseMenu="isCloseMenu" ></Menu>
</template>
<script>
import BaseIcon from "../base/BaseIcon.vue";
import BaseInput from "../base/BaseInput.vue";
import Menu from "../layout/TheMenu.vue";
export default {
    components : {
        Menu,
        BaseIcon,
        BaseInput,
    },
    data() {
        return {
            isCloseMenu: false
        }
    },
    mounted() {
        document.querySelector(".container").style.gridTemplateColumns = "180px calc(100% - 180px)";
    },
    methods: {
        /**
         * Lê Minh Tuyến (19/7/2022)
         * Bấm vào nút sidebar trên thang header
         * Thì sẽ đóng mở menu
         */
        sideBarOnClick() {
            if(!this.isCloseMenu) {
                this.isCloseMenu = true;
                document.querySelector(".container").style.gridTemplateColumns = "56px calc(100% - 56px)";
            } else {
                document.querySelector(".container").style.gridTemplateColumns = "180px calc(100% - 180px)";
                this.isCloseMenu = false;
            }
        }
    }
}
</script>
<style scope>
     @import url(../../assets/css/header.css);
</style>