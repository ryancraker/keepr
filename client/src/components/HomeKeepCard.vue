<script setup>
	import { AppState } from "@/AppState.js";
	import { Keep } from "@/models/Keep.js";
	import { keepsService } from "@/services/KeepsService.js";
	import { vaultKeepsService } from "@/services/VaultKeepsService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { computed } from "vue";
	import { useRoute } from "vue-router";
	const account = computed(() => AppState.account);
	const route = useRoute();
	const keep = defineProps({ keep: { type: Keep, required: true } });
	const focusedVault = computed(() => AppState.focusedVault);
	async function deleteKeep() {
		const confirmed = await Pop.confirm(
			`Are you sure you want to delete ${keep.keep.name}? This will remove it from all vaults.`
		);
		if (!confirmed) return;
		try {
			await keepsService.deleteKeep(keep.keep.id);
			Pop.success(`${keep.keep.name} was successfully deleted!`);
		} catch (error) {
			Pop.error(error);
			logger.error("could not delete keep", error);
		}
	}
	async function deleteVaultKeep() {
		const confirmed = await Pop.confirm(
			`Are you sure you want to delete ${keep.keep.name}? This will only remove it from this vault`
		);
		if (!confirmed) return;
		try {
			await vaultKeepsService.deleteVaultKeep(keep.keep.vaultKeepId);
			Pop.success(`${keep.keep.name} was successfully deleted!`);
		} catch (error) {
			Pop.error(error);
			logger.error("could not delete keep", error);
		}
	}

	async function focusKeep() {
		try {
			logger.log(keep.keep.id);
			keepsService.getKeepById(keep.keep.id);
		} catch (error) {
			Pop.error(error);
			logger.error("could not get keep by id", error);
		}
	}
</script>

<template>
	<section class="keep-card container" :style="{ backgroundImage: `url(${keep.keep.img})` }">
		<button
			@click="deleteKeep()"
			type="button"
			class="delete-button"
			v-if="account?.id == keep.keep?.creatorId && !route.path.includes('vault')"
			title="Delete this keep">
			<i class="mdi mdi-close-circle"></i>
		</button>
		<button
			@click="deleteVaultKeep()"
			type="button"
			class="delete-button"
			v-if="account?.id == focusedVault?.creatorId && route.path.includes('vault')"
			title="Delete this keep">
			<i class="mdi mdi-close-circle"></i>
		</button>
		<div
			class="focus-modal"
			role="button"
			@click="focusKeep()"
			data-bs-target="#focusedKeepModal"
			data-bs-toggle="modal"
			:title="`Open '${keep.keep.name}' in a modal`">
			<img class="img-fluid rounded invisible" :src="keep.keep.img" alt="" />
		</div>
		<div class="row h-100 d-flex flex-grow-1">
			<div class="col-12">
				<div
					class="d-flex justify-content-between align-items-end keep-title p-1 position-relative">
					<span>{{ keep.keep.name }}</span>
					<RouterLink :to="{ name: 'Profile', params: { profileId: keep.keep.creatorId } }">
						<img
							:title="`Go to ${keep.keep.creator.name}'s page`"
							class="creator-pic"
							:src="keep.keep.creator.picture"
							:alt="keep.keep.creator.name"
							:class="{ 'd-none': route.path.includes('profile') }" />
					</RouterLink>
				</div>
			</div>
		</div>
	</section>
</template>

<style lang="scss" scoped>
	.focus-modal {
		display: flex;
		flex-grow: 1;
	}

	.row {
		background: #ffffff;
		background: linear-gradient(179deg, rgba(255, 255, 255, 0) 46%, rgba(5, 0, 0, 0.84) 99%);
		border-bottom-left-radius: 10px;
		border-bottom-right-radius: 10px;
	}

	.keep-card {
		background-position: center;
		background-size: cover;
		border-radius: 10px;
		position: relative;
		color: white;
		font-size: 1rem;
		font-weight: bold;
		text-shadow: 0px 2px 3px black;
		box-shadow: 0px 2px 5px black;
		transition: ease 0.2s;

		&:hover {
			transform: scale(1.05);
			z-index: 1;
			box-shadow: 0px 2px 10px black;
		}

		&:hover .delete-button {
			opacity: 1;
			transition: ease 0.2s;
		}
	}

	.delete-button {
		position: absolute;
		right: -5%;
		top: -5%;
		border-radius: 50%;
		background-color: rgba(255, 255, 255, 0);
		border: none;
		color: rgb(209, 1, 1);
		font-size: 1.5rem;
		opacity: 0;
		transition: ease 0.1s;
	}
</style>
