<script setup>
	import { AppState } from "@/AppState.js";
	import { computed } from "vue";
	const keep = computed(() => AppState.focusedKeep);
</script>

<template>
	<section class="container-fluid">
		<div class="row">
			<div v-if="keep" class="col-12">
				<div class="row">
					<div class="col-6">
						<img class="focused-img" :src="keep.img" :alt="keep.name" />
					</div>
					<div class="col-6">
						<div class="row">
							<div class="col-12">
								<p class="fs-1">{{ keep.name }}</p>
							</div>
							<div class="col-12">
								<span>{{ keep.description }}</span>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<style lang="scss" scoped>
	.modal-backdrop {
		background-color: white;
		opacity: 1;
	}
	.focused-img {
		width: 100%;
		object-fit: cover;
		object-position: center;
	}
</style>
