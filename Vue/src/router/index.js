// 0. Cài đặt vue router : npm install vue-router@4
// 1. import router
import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from "../components/layout/pages/employee/EmployeeList";
import ForOther from "../components/layout/pages/ForOther.vue";
import WarehouseView from "../components/layout/pages/warehouse/WarehouseView";
import INProcess from "../components/layout/pages/warehouse/WareProcess";
import InOutWardList from "../components/layout/pages/warehouse/InOutWardList";

// 2. định nghĩa
const routes = [
    { path: "/employees", component: EmployeeList },
    { path: "/other", component: ForOther },
    {
        path: "/inventory",
        component: WarehouseView,
        children: [
            { path: "INProcess", component: INProcess },
            { path: "InWardOutWardList", component: InOutWardList },
        ],
    },
];

const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHistory(),
    routes: routes, // short for `routes: routes`
});

export default router;