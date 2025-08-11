<script setup>
	import { keepsService } from "@/services/KeepsService.js";
	import { onUnmounted } from "vue";
	onUnmounted(clearFocusedKeep);

	defineProps({
		modalId: { type: String, required: true }
	});
	function clearFocusedKeep() {
		keepsService.clearFocusedKeep();
	}
</script>

<template>
	<div
		class="modal fade"
		:id="modalId + 'Modal'"
		tabindex="-1"
		:aria-labelledby="modalId + 'Label'"
		aria-hidden="true">
		<div class="modal-dialog modal-xl modal-dialog-centered">
			<div class="modal-content">
				<div class="modal-body">
					<slot></slot>
				</div>
			</div>
		</div>
	</div>
</template>

<style lang="scss" scoped>
	.modal-content {
		height: 100%;
		width: 100dvw;
		padding: 0;
		margin: 0;
	}
	.modal-backdrop {
		background-color: white;
		opacity: 1;
	}
	.modal {
		--bs-modal-padding: 0;
		--bs-modal-margin: 0.5rem;
		--bs-modal-header-padding-x: 0;
		--bs-modal-header-padding-y: 0;
		--bs-modal-header-padding: 0;
	}
	.modal-backdrop {
		background-color: rgba(178, 189, 194, 153);
	}
</style>
