<script setup>
	import { AppState } from "@/AppState.js";
	import { accountService } from "@/services/AccountService.js";
	import { vaultKeepsService } from "@/services/VaultKeepsService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { computed, ref, watch } from "vue";
	const keep = computed(() => AppState.focusedKeep);
	const account = computed(() => AppState.account);
	const myVaults = computed(() => AppState.myVaults);
	watch(account, getMyVaults);

	async function getMyVaults() {
		try {
			await accountService.getMyVaults();
		} catch (error) {
			Pop.error(error);
			logger.error(error);
		}
	}

	const vaultKeepData = ref({
		keepId: null,
		vaultId: null
	});

	async function addKeepToVault() {
		try {
			vaultKeepData.value.keepId = keep.value.id;
			logger.log(vaultKeepData.value);
			await vaultKeepsService.addKeepToVault(vaultKeepData.value);
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
								<span class="fs-4">Kept: {{ keep.kept }}|Views: {{ keep.views }}</span>
							</div>
							<div class="col-12">
								<p class="fs-1 text-center">{{ keep.name }}</p>
								<span>{{ keep.description }}</span>
							</div>
							<form
								v-if="account"
								class="col-6 d-flex align-items-end mb-2 gap-1"
								@submit.prevent="addKeepToVault()">
								<div class="d-flex gap-2">
									<label for="vault"></label>
									<select
										v-model="vaultKeepData.vaultId"
										class="form-select"
										aria-label="Select from your vaults"
										id="vault"
										required>
										<option disabled selected>Select a Vault</option>
										<option v-for="vault in myVaults" :key="vault.id" :value="vault.id">
											{{ vault.name }}
										</option>
									</select>
									<button>save</button>
								</div>
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
