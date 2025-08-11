<script setup>
	import { AppState } from "@/AppState.js";
	import { accountService } from "@/services/AccountService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { computed, watch } from "vue";
	const keep = computed(() => AppState.focusedKeep);
	const account = computed(() => AppState.account);
	watch(account, getMyVaults);

	async function getMyVaults() {
		try {
			await accountService.getMyVaults();
		} catch (error) {
			Pop.error(error);
			logger.error(error);
		}
	}
</script>

<template>
	<section class="container-fluid">
		<div class="row">
			<div v-if="keep" class="col-12">
				<div class="row">
					<div class="col-6">
						<img class="focused-img" :src="keep.img" :alt="keep.name" />
					</div>
					<div class="col-6 keep-info">
						<div class="row">
							<div class="col-12 text-center pt-1">
								<span class="fs-4">Kept: {{ keep.kept }}</span>
							</div>
							<div class="col-12">
								<p class="fs-1 text-center">{{ keep.name }}</p>
								<span>{{ keep.description }}</span>
							</div>
							<form class="col-6 d-flex align-items-end mb-2 gap-1">
								<label for=""></label>
								<select class="form-select" aria-label="Default select example">
									<option disabled selected>Select a Vault</option>
									<option value="1">One</option>
								</select>
							</form>
							<div class="d-flex align-items-end justify-content-end gap-3 mb-2 col-6">
								<span class="fw-bold fs-3">{{ keep.creator.name }}</span>
								<img class="creator-pic" :src="keep.creator.picture" :alt="keep.creator.name" />
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
	.keep-info {
		flex-grow: 1;
		display: flex;
		justify-content: space-between;
	}
</style>
