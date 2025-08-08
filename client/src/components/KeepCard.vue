<script setup>
  import { AppState } from '@/AppState.js';
  import { Keep } from '@/models/Keep.js';
  import { keepsService } from '@/services/KeepsService.js';
  import { Pop } from '@/utils/Pop.js';
  import { computed } from 'vue';
  const account = computed(() => AppState.account)

  const keep = defineProps({ keep: { type: Keep, required: true } })
  async function deleteKeep() {
    const confirmed = await Pop.confirm(`Are you sure you want to delete ${keep.keep.name}? This will remove it from all vaults.`)
    if (!confirmed) return
    try {
      await keepsService.deleteKeep(keep.keep.id)
      Pop.success(`${keep.keep.name} was successfully deleted!`)
    }
    catch (error) {
      Pop.error(error);
    }

  }
</script>


<template>
  <div class="keep-card container-fluid" :style="{ backgroundImage: `url(${keep.keep.img})` }">
    <button @click="deleteKeep()" type="button" class="delete-button" v-if="account?.id == keep.keep.creatorId"
      title="Delete this keep"><i class="mdi mdi-close-circle"></i></button>
    <div class="row h-100">
      <div class="col-12">
        <div class="d-flex justify-content-between align-items-end keep-title p-1">
          <span>{{ keep.keep.name }}</span>
          <img class="creator-pic" :src="keep.keep.creator.picture" :alt="keep.keep.creator.name">
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
  .keep-card {
    position: relative;
    height: 100%;
    background-size: cover;
    background-position: center;
    color: white;
    font-size: 1rem;
    font-weight: bold;
    text-shadow: 0px 2px 3px black;
    border-radius: 10px;
    box-shadow: 0px 2px 5px black;

    &:hover .delete-button {
      opacity: 1;
      transition: ease .2s;
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
    transition: ease .1s;
  }

  .keep-title {
    height: 100%;
  }

  .creator-pic {
    height: 50px;
    aspect-ratio: 1/1;
    border-radius: 50%;
  }
</style>