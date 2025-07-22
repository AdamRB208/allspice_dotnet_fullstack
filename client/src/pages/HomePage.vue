<script setup >
import { AppState } from '@/AppState.js';
import CreateModal from '@/components/CreateModal.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { recipeService } from '@/services/RecipeService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


const recipe = computed(() => AppState.recipes)

onMounted(() => {
  getRecipes()
})

async function getRecipes() {
  try {
    await recipeService.getRecipes()
  }
  catch (error) {
    Pop.error(error, 'COULD NOT GET RECIPES!');
    logger.error('Could not get recipes!');

  }
}

</script>

<template>
  <section class="container-fluid">
    <div class="row d-flex justify-content-center">
      <div v-for="recipes in recipe" :key="recipes.id" class="col-md-3 m-2 d-flex justify-content-center flex-column">
        <RecipeCard :recipe="recipes" />
      </div>
    </div>
  </section>
  <RecipeModal>
  </RecipeModal>
  <CreateModal />
</template>

<style scoped lang="scss">

</style>
