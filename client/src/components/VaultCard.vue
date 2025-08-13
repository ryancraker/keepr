<script setup>
	import { Vault } from "@/models/Vault.js";

	defineProps({
		vault: { type: Vault, required: true }
	});
</script>
<template>
	<RouterLink :to="{ name: 'Vault Page', params: { vaultId: vault.id } }">
		<div
			class="container vault-card position-relative"
			:title="`Go to ${vault.name}'s Vault page'`"
			:style="{ backgroundImage: `url(${vault.img})` }">
			<div class="row">
				<div class="col-12">
					<span class="vault-title"
						>{{ vault.name }} <span v-if="vault.isPrivate"><i class="mdi mdi-lock"></i></span
					></span>
				</div>
			</div>
		</div>
	</RouterLink>
</template>

<style lang="scss" scoped>
	img {
		border-radius: 10px;
	}
	.vault-card {
		width: 100%;
		height: 25dvh;
		background-position: center;
		box-shadow: 0px 2px 3px black;
		background-size: cover;
		border-radius: 10px;
		transition: ease 0.2s;
		&:hover {
			transform: scale(1.05);
			box-shadow: 0px 2px 8px black;
		}
	}
	.vault-title {
		position: absolute;
		bottom: 5%;
		left: 5%;
		color: white;
		font-weight: bold;
		border-bottom-left-radius: 10px;
		border-bottom-right-radius: 10px;
	}
	.row {
		background: #ffffff;
		background: linear-gradient(179deg, rgba(255, 255, 255, 0) 46%, rgba(5, 0, 0, 0.84) 99%);
		height: 100%;
		border-bottom-left-radius: 10px;
		border-bottom-right-radius: 10px;
	}
</style>
