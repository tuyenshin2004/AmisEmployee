<template>
      <div class="page">
            <div class="top__menu">
                  <ul>
                        <li v-for="item in topMenuItems" :key="item.title">
                              <router-link :to="item.link">
                                    <div>{{ item.title }}</div>
                              </router-link>
                        </li>
                  </ul>
            </div>
            <router-view></router-view>
      </div>
</template>
<script>
      export default {
            data() {
                  return {
                        topMenuItems: [
                              {
                                    title: "Quy trình",
                                    link: "/inventory/INProcess",
                              },
                              {
                                    title: "Nhập, xuất kho",
                                    link: "/inventory/InWardOutWardList",
                              },
                        ],
                  };
            },
            components: {},
      };
</script>
<style scoped>
      @import url("../../../../assets/css/page/warehouse/warehouse.css");
</style>