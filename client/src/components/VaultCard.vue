<script setup>
	import { Vault } from "@/models/Vault.js";

	defineProps({
		vault: { type: Vault, required: true }
	});
</script>
<template>
	<RouterLink :to="{ name: 'Vault Page', params: { vaultId: vault.id } }">
		<div class="container">
			<div class="row">
				<div class="col-12 position-relative">
					<img class="img-fluid" :src="vault.img" :alt="vault.name" />
					<span class="vault-title">{{ vault.name }}</span>
				</div>
			</div>
		</div>
	</RouterLink>
</template>

<style lang="scss" scoped>
	img {
		border-radius: 10px;
	}
	.vault-title {
		position: absolute;
		bottom: 0;
		left: 10%;
		color: white;
		font-weight: bold;
		background: #ffffff;
		background: linear-gradient(179deg, rgba(255, 255, 255, 0) 46%, rgba(5, 0, 0, 0.84) 99%);
		border-bottom-left-radius: 10px;
		border-bottom-right-radius: 10px;
	}
</style>
