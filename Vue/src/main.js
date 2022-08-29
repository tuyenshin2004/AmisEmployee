import { createApp } from "vue";
import App from "./App.vue";
import router from "./router/index.js";
import axios from "axios";
import VueAxios from "vue-axios";
// import the package
import VueAwesomePaginate from "vue-awesome-paginate";

// import the necessary css file
import "vue-awesome-paginate/dist/style.css";

// import { createRouter, createWebHashHistory } from "vue-router";
// import EmployeeList from "./components/layout/pages/EmployeeList.vue";
// import ForOther from "./components/layout/pages/ForOther.vue";

// const routes = [
//     { path: "/employees", component: EmployeeList },
//     { path: "/other", component: ForOther },
// ];

// const router = createRouter({
//      history: createWebHashHistory(),
//     routes: routes,
// });

const app = createApp(App).use(router);
app.use(VueAxios, axios);
app.use(VueAwesomePaginate);
app.mount("#app");