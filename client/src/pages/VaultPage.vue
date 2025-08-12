<script setup>
	import { AppState } from "@/AppState.js";
	import KeepCard from "@/components/KeepCard.vue";
	import VaultKeepCard from "@/components/VaultKeepCard.vue";
	import { vaultsService } from "@/services/VaultsService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { AxiosError } from "axios";
	import { computed, onMounted } from "vue";
	import { useRoute, useRouter } from "vue-router";
	onMounted(() => {
		getVaultById();
		getVaultKeeps();
	});
	const router = useRouter();
	const route = useRoute();
	const vaultId = route.params.vaultId;
	const vault = computed(() => AppState.focusedVault);
	const keeps = computed(() => AppState.keeps);
	const account = computed(() => AppState.account);

	async function getVaultById() {
		try {
			await vaultsService.getVaultById(vaultId);
		} catch (error) {
			Pop.error(error);
			logger.error(error);
			if (error instanceof AxiosError && error.response.data.includes("Invalid Vault")) {
				router.push({ name: "Home" });
			}
		}
	}
	async function getVaultKeeps() {
		try {
			await vaultsService.getKeepsByVaultId(vaultId);
		} catch (error) {
			Pop.error(error);
			logger.error(error);
		}
	}

	async function deleteVault() {
		const confirmed = await Pop.confirm("Are you sure you want to delete this vault?");
		if (!confirmed) {
			return;
		}
		try {
			await vaultsService.deleteVault(vaultId);
			router.push({ name: "Home" });
			Pop.success("Vault Deleted!");
		} catch (error) {
			Pop.error(error);
			logger.error("could not delete vault" + error);
		}
	}
</script>

<template>
	<section v-if="vault" class="container">
		<div class="row">
			<div class="col-12 d-flex justify-content-center text-center">
				<div
					class="vault-title d-flex justify-content-end flex-column"
					:style="{ backgroundImage: `url(${vault.img})` }">
					<p class="fs-1">{{ vault.name }}</p>
					<RouterLink :to="{ name: 'Profile', params: { profileId: vault.creatorId } }">
						<p class="fs-4 text-white">by {{ vault.creator.name }}</p>
					</RouterLink>
				</div>
			</div>
			<div class="col-12 text-center mt-2">
				<div v-if="vault.creatorId == account?.id" class="dropdown">
					<button
						class="btn btn-secondary dropdown-toggle"
						type="button"
						data-bs-toggle="dropdown"
						aria-expanded="false">
						<i class="mdi mdi-dots-horizontal"></i>
					</button>
					<ul class="dropdown-menu">
						<li><button @click="deleteVault()" class="btn btn-danger">Delete Vault</button></li>
					</ul>
				</div>

				<span>{{ keeps.length }} Keeps</span>
			</div>
			<div class="col-12">
				<div class="row">
					<div v-for="keep in keeps" :key="keep.id" class="col-3">
						<VaultKeepCard :keep />
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<style lang="scss" scoped>
	a {
		text-decoration: none;
	}
	.vault-title {
		background-repeat: none;
		background-position: center;
		background-size: cover;
		color: white;
		font-weight: bold;
		width: 50%;
		height: 25dvh;
		margin-top: 5dvh;
		border-radius: 10px;
		text-shadow: 0px 2px 3px black;
	}
</style>
