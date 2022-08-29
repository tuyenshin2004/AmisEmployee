<template>
      <button class="btn" :class="btnClass">
            <base-tool :content="content" />
            <slot>{{ btnTitle }}</slot>
            <base-icon
                  :size="iconSize"
                  :name="iconName"
                  v-if="hasIcon"
                  class="iconMargin"
            />
      </button>
</template>
<script>
      import BaseIcon from "./BaseIcon.vue";
      import BaseTool from "./BaseTool.vue";
      export default {
            props: {
                  btnClass: String,
                  btnTitle: String,
                  hasIcon: Boolean,
                  iconName: String,
                  iconSize: String,
                  content: String,
            },
            components: {
                  BaseIcon,
                  BaseTool,
            },
      };
</script>
<style scoped>
      @import url(../../assets/css/base/button.css);
      .iconMargin {
            margin-left: 2px;
      }
</style>