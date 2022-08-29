<template>
    <div class="icon" :class="[size,name]"></div>
</template>
<script>
export default {
    props: {
        size : String,
        name : String
    }
}
</script>
<style scoped>
     @import url(../../assets/css/base/icon.css);
</style>