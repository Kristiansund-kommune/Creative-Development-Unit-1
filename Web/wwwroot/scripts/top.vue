<template>
	<nav class="navbar">
		<div class="logo-container">
			<img src="../styles/logo.svg" alt="SYkkel logo" class="logo">
		</div>
	</nav>
</template>
  
  <style scoped>
  .navbar {
	width: 100%;
	background-color: #f7f7f7;
	box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
	top: 0;
	left: 0;
	z-index: 1000;
	display: flex;
	justify-content: left;
	padding: 10px 0;
	margin-bottom: 100px;
  }
  
  .logo-container {
	display: flex;
	align-items: right;
	padding: 10px 25px;
  }
  
  .logo {
	height: 75px;
	width: auto;
  }
  </style>
  
  <script setup lang="ts">
  </script>