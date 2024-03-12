<template>
	<div style="height: 100vh; width: 100vw">
		<Map></Map>	
		<div class="logo-container">
			<img class="logo" src="../styles/logo.svg" alt="logo">
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
</style>

<script setup lang="ts">

import Map from "@/scripts/Map.vue";
</script>