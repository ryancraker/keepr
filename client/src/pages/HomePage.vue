<script setup>
	import { AppState } from "@/AppState.js";
	import KeepCard from "@/components/KeepCard.vue";
	import { keepsService } from "@/services/KeepsService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { computed, onMounted } from "vue";

	onMounted(() => getKeeps());
	const keeps = computed(() => AppState.keeps);
	async function getKeeps() {
		try {
			await keepsService.getKeeps();
		} catch (error) {
			Pop.error(error);
			logger.error("could not get keeps", error);
		}
	}
</script>

<template>
	<section class="container">
		<div class="row">
			<div class="col-md-3 my-2 home-keep" v-for="keep in keeps" :key="keep.id">
				<KeepCard :keep />
			</div>
		</div>
	</section>
</template>

<style scoped lang="scss">
	.modal-backdrop {
		background-color: white;
		opacity: 1;
	}
	.home-keep {
		height: 30dvh;
	}
</style>
