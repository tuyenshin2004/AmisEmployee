<template>
      <!-- Loading -->
      <div class="loading" v-if="isShow">
            <div class="loading__content">
                  <div class="loading__icon"></div>
                  <div class="loading__text">Đang xử lý</div>
            </div>
      </div>
</template>
<script>
      export default {
            props: ["isShow"],
      };
</script>
<style scoped>
      @import url(../../assets/css/base/loading.css);
</style>