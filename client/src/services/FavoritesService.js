import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Favorite } from "@/models/Favorite.js"
import { AppState } from "@/AppState.js"

class FavoritesService {
  async getMyFavorites() {
    const response = await api.get('/account/favorites')
    logger.log('Got Favorites!', response.data)
    const favorites = response.data.map(pojo => new Favorite(pojo))
    AppState.favorites = favorites
  }

  async createFavorite(favoriteData) {
    logger.log('favorite data', favoriteData)
    const response = await api.post('api/favorites', favoriteData)
    logger.log('created favorite!', response.data)
    const favorite = new Favorite(response.data)
    logger.log('favorite', favorite)
    return favorite
  }

}

export const favoritesService = new FavoritesService()