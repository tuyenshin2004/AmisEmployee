<template>
      <div class="face-button" :class="[className]">
            <div class="title-button">{{ title }}</div>
      </div>
</template>
<script>
      export default {
            emits: ["receiptForm"],
            props: {
                  title: {
                        type: String,
                        default: "",
                  },
                  className: {
                        type: String,
                        default: "",
                  },
            },
            methods: {
                  faceButtonOnclick(title) {
                        if (title == "Nhập kho") {
                              alert("Hiện phiếu nhập kho");
                              this.$emits("receiptForm", true);
                        }
                  },
            },
      };
</script>
<style scoped>
      .face-button {
            padding-top: 60px;
            position: absolute;
            transition: all 0.2s ease;
            text-align: center;
      }

      .face-button:hover {
            cursor: pointer;
            box-shadow: 0 3px 6px rgb(0 0 0 / 25%);
      }

      .title-button {
            display: flex;
            justify-content: center;
            height: 100%;
            /* align-items: flex-end; */
            padding-bottom: 10px;
      }
      .production-order {
            top: 25px;
            left: 31px;
            width: 120px !important;
      }
      .assemble-dismantling-order {
            top: 215px;
            left: 25px;
            width: 110px !important;
            height: unset !important;
            padding: 0 10px;
            padding-top: 65px !important;
      }
      .export-inventory {
            top: 42px;
            left: 232px;
            width: 100px !important;
      }

      .transfer {
            top: 42px;
            left: 374px;
            width: 100px !important;
      }
      .in-inward {
            top: 222px;
            left: 250px;
            width: 100px !important;
      }
      .outward-price {
            left: 385px;
            width: 125px !important;
            top: 222px;
      }
      .audit {
            left: 540px;
            top: 222px;
            width: 100px !important;
      }
</style>