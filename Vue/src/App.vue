<template>
    <body>
        <div class="container">
            <TheHeader />
            <TheContent />
        </div>
    </body>
</template>

<script>
import TheHeader from "./components/layout/TheHeader.vue";
import TheContent from "./components/layout/TheContent.vue";

export default {
  name: 'App',
  components: {
    TheHeader,
    TheContent
},
  
}
</script>

<style>
 @import url(./assets/css/main.css);
  @import url(./assets/css/base/scrollbar.css);
</style>
