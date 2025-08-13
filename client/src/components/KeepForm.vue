<script setup>
	import { keepsService } from "@/services/KeepsService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { Modal } from "bootstrap";
	import { ref } from "vue";

	const editableKeepForm = ref({
		name: null,
		description: null,
		img: null
	});

	async function submitKeep() {
		try {
			logger.log(editableKeepForm.value);
			const keepData = editableKeepForm.value;
			await keepsService.createKeep(keepData);
			Modal.getOrCreateInstance("#keepModal").hide();
			Pop.success("Keep Submitted!");
		} catch (error) {
			Pop.error(error);
			logger.error("Unable to post keep", error);
		}
	}
</script>

<template>
	<section class="container">
		<div class="row">
			<div class="col-12">
				<form @submit.prevent="submitKeep()">
					<div class="row">
						<div class="col-6 img-preview">
							<label for="img-preview" class="mb-1">Image Preview</label>
							<img
								id="img-preview"
								v-if="editableKeepForm.img?.length > 10"
								class="img-fluid img-preview"
								:src="editableKeepForm.img"
								alt="Preview of your keep" />
						</div>
						<div class="col-6">
							<label for="name" class="mb-1">Name:</label>
							<input
								v-model="editableKeepForm.name"
								id="name"
								type="text"
								name="name"
								title="Keep Name"
								maxlength="255"
								required />
						</div>
						<div class="col-6">
							<label for="description" class="mb-1">Description:</label>
							<input
								v-model="editableKeepForm.description"
								type="text"
								id="description"
								name="description"
								title="Keep Description"
								maxlength="1000"
								required />
						</div>
						<div class="col-6 mb-2">
							<label for="img" class="mb-1">Image Url:</label>
							<input
								v-model="editableKeepForm.img"
								type="url"
								id="img"
								name="img"
								title="Keep Image"
								maxlength="1000"
								required />
						</div>
						<div class="text-end">
							<button type="submit" class="keep-button">Submit Keep</button>
						</div>
					</div>
				</form>
			</div>
		</div>
	</section>
</template>

<style lang="scss" scoped>
	.modal-backdrop {
		background-color: white;
		opacity: 1;
	}
	.img-preview {
		height: 20dvh;
	}

	.keep-button {
		background-color: black;
		color: white;
		border-radius: 15px;
		border: none;
		padding: 0.5rem;
		font-weight: bold;
	}
</style>
