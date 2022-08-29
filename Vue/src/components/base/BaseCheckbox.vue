<template>
    <label class="container-checkbox">
        <input type="checkbox" class="checkbox">
        <span class="checkmark"></span>
    </label>
    <span class="checkbox__span" v-if="hasSpan">{{ nameSlot }}</span>
</template>
<script>
export default {
    props: {
        nameSlot : String,
        hasSpan : Boolean
    }
}
</script>
<style scoped>
     @import url(../../assets/css/base/checkbox.css);
</style>