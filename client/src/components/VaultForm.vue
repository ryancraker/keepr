<script setup>
	import { vaultsService } from "@/services/VaultsService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { Modal } from "bootstrap";
	import { ref } from "vue";
	import { useRouter } from "vue-router";

	const editableVaultForm = ref({
		name: "",
		description: "",
		isPrivate: false,
		img: ""
	});

	const router = useRouter();

	async function createVault() {
		try {
			logger.log(editableVaultForm.value);
			const vault = await vaultsService.createVault(editableVaultForm.value);
			Modal.getOrCreateInstance("#vaultModal").hide();
			editableVaultForm.value = {
				name: "",
				description: "",
				isPrivate: false,
				img: ""
			};
			router.push({ name: "Vault Page", params: { vaultId: vault.id } });
		} catch (error) {
			Pop.error(error);
			logger.error(error);
		}
	}
</script>

<template>
	<section class="container">
		<div class="row">
			<div class="col-12">
				<form @submit.prevent="createVault()">
					<div class="row">
						<div class="col-6 img-preview">
							<label for="img-preview" class="mb-1">Image Preview</label>
							<img
								v-if="editableVaultForm.img.length > 5"
								id="img-preview"
								class="img-fluid"
								:src="editableVaultForm.img"
								alt="Preview of your vault" />
						</div>
						<div class="col-6">
							<label for="name" class="mb-1">Name:</label>
							<input
								v-model="editableVaultForm.name"
								id="name"
								type="text"
								name="name"
								title="Vault Name"
								maxlength="255"
								required />
						</div>
						<div class="col-6">
							<label for="description" class="mb-1">Description:</label>
							<input
								v-model="editableVaultForm.description"
								type="text"
								id="description"
								name="description"
								title="Vault Description"
								maxlength="1000"
								required />
						</div>
						<div class="col-6 mb-2">
							<label for="img" class="mb-1">Image Url:</label>
							<input
								v-model="editableVaultForm.img"
								type="url"
								id="img"
								name="img"
								title="Vault Image"
								maxlength="1000"
								required />
						</div>
						<div class="form-check col-6">
							<input
								v-model="editableVaultForm.isPrivate"
								class="form-check-input"
								type="checkbox"
								value="false"
								id="checkDefault" />
							<label class="form-check-label" for="checkDefault">Private?</label>
						</div>
						<div class="text-end">
							<button type="submit" class="vault-button">Submit Vault</button>
						</div>
					</div>
				</form>
			</div>
		</div>
	</section>
</template>

<style lang="scss" scoped>
	.vault-button {
		border: none;
		background-color: var(--bs-secondary);
	}
	.img-preview img {
		height: 80%;
		object-fit: cover;
		object-position: center;
	}
</style>
