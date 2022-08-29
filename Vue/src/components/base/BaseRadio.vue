<template>
      <label class="container-radio" v-for="gender in genders" :key="gender.id"
            >{{ gender.name }}
            <input
                  type="radio"
                  :checked="genderVal == gender.id"
                  name="radio"
                  @click="checkRadio(gender)"
            />
            <span class="checkmark-radio"></span>
      </label>
</template>
<script>
      export default {
            props: {
                  genderVal: {
                        type: Number,
                        default: 2,
                  },
            },
            emits: ["genderSelect"],
            data() {
                  return {
                        genders: [
                              {
                                    name: "Nam",
                                    id: 1,
                              },
                              {
                                    name: "Nữ",
                                    id: 0,
                              },
                              {
                                    name: "Khác",
                                    id: 2,
                              },
                        ],
                  };
            },
            methods: {
                  checkRadio(gender) {
                        this.$emit("genderSelect", gender);
                  },
            },
      };
</script>
<style scoped>
      @import url(../../assets/css/base/radio.css);
</style>