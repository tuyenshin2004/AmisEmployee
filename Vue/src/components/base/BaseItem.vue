<template>
    <div class="menu-item">
        <base-icon size="icon--24" :name="name" />
        <span v-if="!closeMenu" >{{title}}</span>
    </div>
</template>
<script>
import BaseIcon from "./BaseIcon.vue";
export default {
    props: {
        name : String,
        title : String,
        closeMenu : Boolean
    },
    components : {
    BaseIcon
}
}
</script>
<style scoped>
    .menu-item {
    height: 44px;
    width: 100%;
    box-sizing: border-box;
    display: flex;
    align-items: center;
    padding-left: 16px;
    /* đảm bảo chắc chắn độ rộng */
    box-sizing: border-box;
    color: #ffffff;
    cursor: pointer;
    border-left: 4px solid transparent;
}

.menu-item:hover {
    background-color: #58595d;
    color: #ffffff;
}

.menu-item:active {
    background-color: #58595d;
    color: #ffffff;
    /* text style medium */
    font-weight: 500;
}
.menu-item__icon {
    /* căn chiều ngang , dọc của background */
    background-position: center center;
    margin-right: 10px;
    font-size: 100px;
}
.menu .router-link-active .menu-item{
    background-color: #58595d;
    color: #ffffff;
    /* text style medium */
    font-weight: 500;
    border-left: 4px solid #2ca01c;
}

</style>