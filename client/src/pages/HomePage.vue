<script setup>
	import { AppState } from "@/AppState.js";
	import HomeKeepCard from "@/components/HomeKeepCard.vue";
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
	<section v-if="keeps" class="container">
		<div class="row">
			<div class="col-12">
				<div class="masonry-container mt-2">
					<div class="mb-4" v-for="keep in keeps" :key="keep.id">
						<HomeKeepCard :keep />
					</div>
				</div>
			</div>
		</div>
	</section>
	<section v-else>
		<h1>LOADING...<i class="mdi mdi-loading mdi-spin"></i></h1>
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
