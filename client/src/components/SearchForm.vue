<script setup>
import { AppState } from '@/AppState.js';
import { recipeService } from '@/services/RecipeService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';


const searchQuery = computed(() => AppState.currentSearchQuery)
const editableSearch = ref('')

async function searchRecipes() {
  try {
    await recipeService.searchRecipes(editableSearch.value)
    editableSearch.value = ''
  }
  catch (error){
    Pop.error(error, 'COULD NOT SEARCH!');
    logger.log('Could not search!', error)
  }
}

</script>


<template>
  <span class="ms-auto me-3">
    <form @submit.prevent="searchRecipes()" class="d-flex">
      <label for="recipeCategory" class="form-label"></label>
      <input v-model="editableSearch" class="form-control" list="recipeCategory" id="recipeCategory"
        placeholder="Category search..." type="text" aria-label="recipeCategory" aria-describedby="searchButton">
      <datalist id="recipeCategory">
        <option>{{ searchQuery.toLowerCase() }}</option>
      </datalist>
      <span>
        <button type="submit" class="btn btn-primary" id="searchButton"><i class="mdi mdi-magnify"></i></button>
      </span>
    </form>
  </span>
</template>


<style lang="scss" scoped>

</style>