<template>
      <div class="menu">
            <div class="menu__top pointer">
                  <div class="menu-top__sidebar">
                        <base-icon class="icon__toggle" size="icon--24" />
                  </div>
                  <div class="menu-top__logo icon__logo"></div>
            </div>
            <div class="menu--scroll">
                  <div class="menu__bottom">
                        <router-link to="/other">
                              <base-item
                                    name="icon__dashboard"
                                    title="Tổng quan"
                                    :closeMenu="isCloseMenu"
                              />
                        </router-link>
                        <router-link to="/employees">
                              <base-item
                                    name="icon__cash"
                                    title="Tiền mặt"
                                    :closeMenu="isCloseMenu"
                              />
                        </router-link>
                        <router-link to="/inventory/INProcess">
                              <base-item
                                    name="icon__warehouse"
                                    title="Kho"
                                    :closeMenu="isCloseMenu"
                              />
                        </router-link>
                        <base-item
                              v-for="item in menuItems"
                              :key="item.title"
                              :name="item.name"
                              :title="item.title"
                              :closeMenu="isCloseMenu"
                        />
                  </div>
            </div>
      </div>
</template>
<script>
      import BaseItem from "../base/BaseItem.vue";
      import BaseIcon from "../base/BaseIcon.vue";
      export default {
            components: {
                  BaseItem,
                  BaseIcon,
            },
            props: {
                  isCloseMenu: {
                        type: Boolean,
                        default: false,
                  },
            },
            data() {
                  return {
                        menuItems: [
                              {
                                    title: "Tiền gừi",
                                    name: "icon__deposit",
                              },
                              {
                                    title: "Mua hàng",
                                    name: "icon__buy",
                              },
                              {
                                    title: "Bán hàng",
                                    name: "icon__sell",
                              },
                              {
                                    title: "Quản lý hoá đơn",
                                    name: "icon__bill",
                              },
                              {
                                    title: "Công cụ dụng cụ",
                                    name: "icon__tool",
                              },
                              {
                                    title: "Tài sản cố định",
                                    name: "icon__assets",
                              },
                              {
                                    title: "Tiền lương",
                                    name: "icon__wage",
                              },
                              {
                                    title: "Thuế",
                                    name: "icon__tax",
                              },
                              {
                                    title: "Giá thành",
                                    name: "icon__price",
                              },
                              {
                                    title: "Tổng hợp",
                                    name: "icon__synthetic",
                              },
                              {
                                    title: "Ngân sách",
                                    name: "icon__budget",
                              },
                              {
                                    title: "Báo cáo",
                                    name: "icon__report",
                              },
                              {
                                    title: "Phân tích tài chính",
                                    name: "icon__analysis",
                              },
                        ],
                  };
            },
      };
</script>
<style scope>
      @import url(../../assets/css/menu.css);
      .icon__logo {
            background-image: url(../../assets/Resource/img/logo_event_trungthu.090d014b.svg);
      }

      .menu--scroll {
            position: absolute;
            top: 48px;
            left: 0;
            width: 100%;
            height: calc(100vh - 48px);
            background: url(../../assets/Resource/img/bg_trung_thu.00497630.png)
                  no-repeat bottom;
            background-size: contain;
      }
</style>