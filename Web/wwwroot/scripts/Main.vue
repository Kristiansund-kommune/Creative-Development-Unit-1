<template>
	<div style="height: 100vh; width: 100vw">
		<Map></Map>
		<div class="logo-container">
			<img class="logo" src="../styles/logo.svg" alt="logo">
		</div>
		<button @click="showModal = true">Show Modal</button>
		<div v-if="showModal" class="modal">
			<div class="modal-content">				
				<div>
					<span class="close" @click="showModal = false">&times;</span>
				</div>
				<h2>Parkering</h2>
				<div class="p-3"></div>
				<p>Plasser som er tillgejenlig:</p>
				<div class="border2 m-3 p-4">
					<div v-for="div in divs" :key="div.id" class="selectable-div" :class="{'red': div.selected, 'green': !div.selected}" @click="toggleSelection(div.id)" v-html="div.name">
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped>
.logo-container {
  position: absolute;
  top: 0;
  left: 0;
  display: flex;
  align-items: right;
  padding: 10px 25px;
  background-color: rgb(240, 239, 239);
}

.logo {
  height: 50px;
  width: auto;
}

.modal {
  display: block;
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgba(0, 0, 0, 0.4);
}

.border2 {
	border-bottom: 0 !important;
	padding: 15px;
	border: 1PX solid black;
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(60px, 0.5fr)); /* Creates a responsive grid where each cell has a min width of 120px */
  gap: 10px; /* Adjusts the gap between grid items */
}


.modal-content {
  background-color: #fefefe;
  margin: 15% auto;
  padding: 2%;
  border: 1px solid #888;
  width: 80%;
}

.close {
  color: #aaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: black;
  text-decoration: none;
  cursor: pointer;
}

.selectable-div {
  text-align: center;
  padding: 10px;
  cursor: pointer;
  color: white; /* White text */
  height: 60px;
  width: 60px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.red {
  background-color: rgb(185, 17, 17);
}

.green {
  background-color: rgb(45, 128, 45);
}
</style>

<script setup lang="ts">
import { ref } from "vue";
import Map from "@/scripts/Map.vue";

const showModal = ref(false);

interface Div {
	id: number;
	name: string;
	selected: boolean;
}

const divs = ref<Div[]>([
	{ id: 1, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 2, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 3, name: '<i class="fa-solid fa-bicycle"></i>', selected: true },
	{ id: 4, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 5, name: '<i class="fa-solid fa-bicycle"></i>', selected: true },
	{ id: 6, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 7, name: '<i class="fa-solid fa-bicycle"></i>', selected: true },
	{ id: 8, name: '<i class="fa-solid fa-bicycle"></i>', selected: true },
	{ id: 9, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 0, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 9, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 42, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 29, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
	{ id: 20, name: '<i class="fa-solid fa-bicycle"></i>', selected: false },
]);

const toggleSelection = (id: number) => {
	const div = divs.value.find(d => d.id === id);
	if (div) {
		div.selected = !div.selected;
	}
};
</script>