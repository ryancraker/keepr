<script setup>
	import { vaultsService } from "@/services/VaultsService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { onMounted } from "vue";
	import { useRoute } from "vue-router";
	onMounted(() => {
		getVaultById();
		getVaultKeeps();
	});
	const route = useRoute();
	const vaultId = route.params.vaultId;

	async function getVaultById() {
		try {
			await vaultsService.getVaultById(vaultId);
		} catch (error) {
			Pop.error(error);
			logger.error(error);
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
</script>

<template>
	<section class="container">
		<div class="row">
			<div class="col-12">this is the vault page</div>
		</div>
	</section>
</template>

<style lang="scss" scoped></style>
