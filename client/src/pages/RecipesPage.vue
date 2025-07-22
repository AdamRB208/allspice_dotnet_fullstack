<script setup>
import { AppState } from '@/AppState.js';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { recipeService } from '@/services/RecipeService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


const account = computed(() => AppState.account)
const recipe = computed(() => AppState.recipes)

onMounted(() => {
  getMyRecipes()
})

async function getMyRecipes() {
  try {
    await recipeService.getMyRecipes()
  }
  catch (error){
    Pop.error(error, 'COULD NOT GET YOUR RECIPES!');
    logger.error('Could not get users recipes!', error)
  }
}

</script>


<template>
  <section class="container-fluid">
    <div class="row d-flex justify-content-center">
      <div v-for="recipe in recipe" :key="recipe.id" class="col-md-3 m-2">
        <RecipeCard :recipe="recipe" />
        <div class="d-flex justify-content-between align-items-center p-2">
        </div>
      </div>
    </div>
  </section>
  <RecipeModal />
</template>


<style lang="scss" scoped>

</style>