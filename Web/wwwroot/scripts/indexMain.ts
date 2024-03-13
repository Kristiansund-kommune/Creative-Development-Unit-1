import "@/core";
import { createApp } from "vue";
import FloatingVue from "floating-vue";
import {createRouter, createWebHistory, RouteRecordRaw} from "vue-router";
import NotFound from "@/scripts/notFound.vue";
import Index from "@/scripts/index.vue";
import Layout from "@/scripts/layout.vue";
import Main from "@/scripts/Main.vue";
import Login from "@/scripts/Login.vue";


const routes : RouteRecordRaw[] = [
	{ path: "/:pathMatch(.*)", component: NotFound },
	{ path: "/", component: Index },
	{ path: "/main/:id", component: Main},
	{ path: "/login", component: Login}
];

const router = createRouter({
	history: createWebHistory("/#/"),
	routes
});

const app = createApp(Layout);
app.use(FloatingVue);
app.use(router);
app.mount("#entrypoint");
