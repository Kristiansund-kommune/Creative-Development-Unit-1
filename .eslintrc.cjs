const ignore = 0;
const warn = "warn";
const error = "error";

// https://github.com/typescript-eslint/typescript-eslint/tree/master/packages/eslint-plugin/docs/rules

module.exports = {
	parser: "vue-eslint-parser",
	parserOptions: {
		parser: "@typescript-eslint/parser",
		sourceType: "module"
	},
	extends: [
		"plugin:vue/base",
		"plugin:vue/vue3-essential",
		"plugin:vue/vue3-strongly-recommended",
		"plugin:vue/vue3-recommended",
		"eslint:recommended",
		"plugin:@typescript-eslint/recommended",
	],
	rules: {
		"@typescript-eslint/no-inferrable-types": ignore,
		"@typescript-eslint/no-explicit-any": ignore,
		"@typescript-eslint/explicit-module-boundary-types": ignore,
		"@typescript-eslint/ban-ts-comment": ignore,
		"@typescript-eslint/no-empty-function": ignore,
		"@typescript-eslint/no-extra-semi": warn,
		"vue/html-indent": [warn, "tab"],
		"vue/html-self-closing": ignore,
		"vue/max-attributes-per-line": ignore,
		"vue/attributes-order": ignore,
		"vue/html-closing-bracket-newline": ignore,
		"vue/singleline-html-element-content-newline": ignore,
		"vue/html-closing-bracket-spacing": warn,
		"vue/attribute-hyphenation": ignore,
		"vue/mustache-interpolation-spacing": warn,
		"vue/multiline-html-element-content-newline": ignore,
		"vue/no-v-html": error,
		"vue/require-v-for-key": error,
		"vue/no-use-v-if-with-v-for": error,
		"vue/no-unused-vars": warn,
		"vue/valid-v-model": error,
		"vue/valid-v-for": error,
		"vue/valid-v-bind": error,
		"vue/no-textarea-mustache": error,
		"vue/no-multi-spaces": warn,
		"no-undef": warn,
		"no-empty": warn,
		"prefer-const": warn,
		"no-extra-boolean-cast": warn,
		"no-irregular-whitespace": warn,
		"semi": "off",
		"@typescript-eslint/semi": [warn],
		"quotes": "off",
		"@typescript-eslint/quotes": [warn, "double", { avoidEscape: true, allowTemplateLiterals: true }],
		"brace-style": "off",
		"@typescript-eslint/brace-style": [warn, "stroustrup"],
		"curly": [warn, "all"],
		"no-warning-comments": [warn, { terms: ["todo"], location: "anywhere" }], // for å synliggjøre todo-comments
		eqeqeq: [error, "always"],
		"no-unused-expressions": error,
		"no-var": warn,
		"@typescript-eslint/no-empty-interface": warn,
		"vue/multi-word-component-names": ignore,
		"prefer-rest-params": ignore,
		"vue/component-definition-name-casing": [warn, "kebab-case"],
		"no-console": [warn, { allow: ["error"] }],
		"vue/first-attribute-linebreak": ignore,
		"vue/component-tags-order": [warn, { "order": ["template", "style", "script"] }],
	}
};
