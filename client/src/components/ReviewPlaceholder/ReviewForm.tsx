import React from 'react';
import './reviewForm.scss';

const ReviewForm:React.FC = () => (
  <div className="review">
    <textarea placeholder="Write your review…" disabled />
  </div>
);

export default ReviewForm;
