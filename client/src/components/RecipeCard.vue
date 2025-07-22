<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientService } from '@/services/IngredientService.js';
import { recipeService } from '@/services/RecipeService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()
const account = computed(() => AppState.account)

defineProps({
  recipe: { type: Recipe, required: true }
})

async function setActiveRecipe(recipe, recipeId) {
  try {
    AppState.activeRecipe = recipe
    await recipeService.setActiveRecipe(recipe)
    logger.log('Setting active recipe', recipe)
    recipeId = route.params.recipeId || recipeId
    logger.log('Recipe Id', recipeId)
    AppState.activeRecipe.id = recipeId
    logger.log('Getting Ingredients', recipeId)
    await ingredientService.getIngredientByRecipeId(recipeId)

  }
  catch (error) {
    Pop.error(error, 'COULD NOT SET ACTIVE RECIPE!');
    logger.error('Could not set active recipe!', error)
  }
}

</script>


<template>
  <div>
    <div @click="setActiveRecipe(recipe, recipe.id)" class="recipe-card" data-bs-toggle="modal"
      data-bs-target="#recipeModal">
      <div class="card-img">
        <img :src="recipe.img" :alt="`image of ${recipe.title}`" class="recipe-img" type="button">
        <div class="card-text">
          <span class="ms-2 mt-2 text-center">{{ recipe.title }}</span>
          <span class="badge rounded-3 p-1 text-center mt-1 me-1" style="max-height: 22%;">
            {{ recipe.category }}
          </span>
        </div>
        <div v-if="account" class="card-icon">
          <i class="mdi mdi-heart text-danger fs-4" type="button"></i>
          <!-- <i class="mdi mdi-heart-outline text-white fs-4" type="button"></i> -->
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.recipe-img {
  object-fit: cover;
  width: 100%;
  height: 300px;
}

.card-img {
  position: relative;
  width: 100%;
}

.card-text {
  position: absolute;
  top: 0;
  color: white;
  font-weight: 500;
  display: flex;
  justify-content: space-between;
  width: 100%;
  text-shadow: 0 0 3px #242222;
  background: linear-gradient(180deg, #05050586 20%, transparent);
  height: 50%;
}

.card-icon {
  position: absolute;
  bottom: 0;
  display: flex;
  width: 100%;
  text-shadow: 0 0 3px #242222;
  background: linear-gradient(0deg, #1716167b 20%, transparent);
  padding: 5px;
  height: 15%;
}

.badge {
  background-color: rgba(11, 11, 11, 0.501);
}
</style>