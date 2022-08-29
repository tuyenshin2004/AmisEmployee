<template>
      <div class="tooltip" v-if="content ? true : false">
            <span class="tooltiptext">{{ content }}</span>
      </div>
</template>
<script>
      export default {
            props: {
                  content: {
                        type: String,
                        default: "",
                  },
            },
      };
</script>
<style scoped>
      .tooltip {
            margin-top: 28px;
      }
      .tooltip .tooltiptext {
            display: none;
            background-color: black;
            color: #fff;
            text-align: center;
            border-radius: 6px;
            padding: 2px 4px;

            /* Position the tooltip */
            position: absolute;
            z-index: 10;
      }

      div:hover > .tooltip .tooltiptext {
            display: block;
      }
      label:hover > .tooltip .tooltiptext {
            display: block;
            font-weight: 200;
      }
      button:hover > .tooltip .tooltiptext {
            display: block;
      }
      label > .tooltip {
            margin-top: 0px;
      }
</style>