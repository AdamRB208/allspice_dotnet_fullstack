import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"


class RecipeService {
  async getMyRecipes() {
    const response = await api.get('/account/recipes')
    logger.log('Got users recipes!', response.data)
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }
  async createRecipe(recipeData) {
    const response = await api.post('api/recipes', recipeData)
    logger.log('Created Recipe!', response.data)
    const recipe = new Recipe(response.data)
    return recipe
  }

  async getRecipes() {
    const response = await api.get('api/recipes')
    logger.log('Got Recipes!', response.data)
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }

  setActiveRecipe(activeRecipe) {
    AppState.activeRecipe = activeRecipe
    logger.log(activeRecipe)
  }

  async editRecipe(recipeId, recipeData) {
    const response = await api.put(`api/recipes/${recipeId}`, recipeData)
    logger.log('Updated recipe!', response.data)
    const recipe = AppState.recipes
    const index = recipe.findIndex(recipe => recipe.id == recipeId)
    recipe.splice(index, 1, recipeData)
  }

  async deleteRecipe(recipeId) {
    const response = await api.delete(`api/recipes/${recipeId}`)
    logger.log('Deleted Recipe!', response.data)
    const recipe = AppState.recipes
    const index = recipe.findIndex(recipe => recipe.id == recipeId)
    recipe.splice(index, 1)
  }

}

export const recipeService = new RecipeService()