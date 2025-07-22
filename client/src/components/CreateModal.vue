<script setup>
import { recipeService } from '@/services/RecipeService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';


const editableRecipeData = ref({
  title: '',
  category: '',
  img: '',
  instructions: ''
})

async function createRecipe() {
  try {
    const recipeData = editableRecipeData.value
    await recipeService.createRecipe(recipeData)
    editableRecipeData.value = {
      title: '',
      category: '',
      img: '',
      instructions: ''
    }
  }
  catch (error){
    Pop.error(error, 'COULD NOT CREATE RECIPE!');
    logger.error('Could not create recipe!', error)
  }
}


</script>


<template>
  <div class="modal" tabindex="-1" id="createModal" aria-labelledby="createModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="createModalLabel">Create A New Recipe!</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createRecipe">
            <div class="mb-3">
              <label for="recipeTitle" class="pe-2">Recipe Title </label>
              <input v-model="editableRecipeData.title" type="text" id="recipeTitle" name="title"
                placeholder="Name your recipe" required maxlength="255" minLength="1" class="d-block">
            </div>
            <label for="recipeCategory" class="pe-2" required>Category </label>
            <select v-model="editableRecipeData.category" name="category" id="recipeCategory" required class="d-block">
              <option selected>Select a Category</option>
              <option value="breakfast">Breakfast</option>
              <option value="lunch">Lunch</option>
              <option value="dinner">Dinner</option>
              <option value="snack">Snack</option>
              <option value="desert">Desert</option>
              <option value="mexican">Mexican</option>
              <option value="cheese">Cheese</option>
              <option value="italian">Italian</option>
              <option value="soup">Soup</option>
              <option value="coffee">Coffee</option>
            </select>
            <div class="mb-3 mt-3">
              <label for="recipeImg">Add Recipe Image</label>
              <div>
                <label for="recipeImg"></label>
                <input v-model="editableRecipeData.img" type="url" id="recipeImg" name="imgUrl" required
                  maxlength="1000" placeholder="Recipe Image URL...">
              </div>
            </div>
            <div class="mb-3">
              <label for="recipeInstructions" class="pe-2 d-block">Recipe Instructions</label>
              <textarea v-model="editableRecipeData.instructions" id="recipeInstructions" name="instructions" rows="5"
                placeholder="How to make your recipe..." maxlength="5000" class="w-100"></textarea>
              <div class="d-flex justify-content-end m-2">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <button type="submit" class="btn btn-primary ms-2">Create <i class="mdi mdi-plus"></i></button>
              </div>
            </div>
          </form>
        </div>
        <div class="modal-footer">
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

</style>