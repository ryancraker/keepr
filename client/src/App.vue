<script setup>
	import Navbar from "./components/Navbar.vue";
	import { RouterView } from "vue-router";
</script>

<template>
	<header>
		<Navbar />
	</header>
	<main>
		<RouterView />
	</main>
	<footer class="text-center">Made with patience</footer>
</template>

<style lang="scss">
	.modal-backdrop {
		background-color: white;
		opacity: 1;
	}
</style>
