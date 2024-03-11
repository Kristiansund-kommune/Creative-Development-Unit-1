import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

import packageJson from "./package.json";
const dependencies = [];
for (const dep of Object.keys(packageJson.dependencies)) {
	dependencies.push(dep);
}

// https://vitejs.dev/config/
export default defineConfig({
	base: "",
	root: "Web/wwwroot",
	plugins: [
		vue()
	],
	resolve: {
		alias: {
			"@": fileURLToPath(new URL("./Web/wwwroot", import.meta.url))
		}
	},
	optimizeDeps: {
		include: dependencies,
	},
	build: {
		manifest: true,
		cssCodeSplit: false,
		target: "modules",
		rollupOptions: {
			input: [
				"scripts/indexMain.ts",
				"scripts/secure/secureIndexMain.ts",
			].map(str => "Web/wwwroot/" + str),
			output: {
				entryFileNames: `assets/[name].js`,
				chunkFileNames: `assets/[name].js`,
				assetFileNames: `assets/[name].[ext]`
			}
		}
	}
});
