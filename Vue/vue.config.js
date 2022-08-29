// const { defineConfig } = require("@vue/cli-service");
// module.exports = defineConfig({
//     transpileDependencies: true,
//     configureWebpack: {
//         devServer: {
//             headers: { "Access-Control-Allow-Origin": "*" },
//             proxy: "https://localhost:7157/api/v1/",
//         },
//     },
// });
module.exports = {
    devServer: {
        headers: { "Access-Control-Allow-Origin": "*" },
        proxy: "https://localhost:7157/",
    },
};