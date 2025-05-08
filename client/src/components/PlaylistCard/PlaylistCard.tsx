import './playlistCard.scss';
import FavoriteIcon   from '@mui/icons-material/FavoriteBorder';
import VisibilityIcon from '@mui/icons-material/VisibilityOutlined';
import WatchIcon      from '@mui/icons-material/AccessTime';

interface Props{
  title : string;
  count : number;
}

const icon = (title:string)=>{
  if (/fav/i.test(title))     return <FavoriteIcon/>;
  if (/watch\s+later/i.test(title)) return <WatchIcon/>;
  if (/watch(ed)?/i.test(title))    return <VisibilityIcon/>;
  return null;
};

const colorClass = (title:string)=>{
  if (/fav/i.test(title))         return 'pl-fav';
  if (/watch\s+later/i.test(title)) return 'pl-later';
  if (/watch(ed)?/i.test(title))    return 'pl-watched';
  return 'pl-custom';
};

const PlaylistCard:React.FC<Props>=({title,count})=>(
  <article className={`pl-card ${colorClass(title)}`}>
    <div className="pl-bg"/>
    <div className="pl-content">
      {icon(title)}
      <h3>{title}</h3>
      <span>{count}</span>
    </div>
  </article>
);

export default PlaylistCard;
