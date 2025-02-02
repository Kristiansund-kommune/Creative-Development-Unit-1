import "@/core";
import { createApp } from "vue";
import FloatingVue from "floating-vue";
import { createRouter, createWebHistory } from "vue-router";
import NotFound from "@/scripts/notFound.vue";
import SecureIndex from "@/scripts/secure/secureIndex.vue";
import Layout from "@/scripts/layout.vue";

const routes = [
	{ path: "/:pathMatch(.*)", component: NotFound },
	{ path: "/", component: SecureIndex }
];

const router = createRouter({
	history: createWebHistory("/Secure/#/"),
	routes
});

const app = createApp(Layout);
app.use(FloatingVue);
app.use(router);
app.mount("#entrypoint");
